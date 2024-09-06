---------------------------------------------------------------
--- DECLARACIÓN Y CREACIÓN DE ELEMENTOS DE LA BASE DE DATOS ---
---------------------------------------------------------------


--- Eliminar la base de datos si ya existe ---
IF EXISTS(SELECT * FROM sys.databases WHERE name='TARJETASCORE')
BEGIN
    DROP DATABASE TARJETASCORE;
END

--- Creación la base de datos ---
CREATE DATABASE TARJETASCORE;
USE TARJETASCORE;

--- Eliminar la tabla PARAMETROS si ya existe ---
IF OBJECT_ID('PARAMETROS', 'U') IS NOT NULL
BEGIN
    DROP TABLE PARAMETROS;
END

--- Creación la tabla PARAMETROS ---
CREATE TABLE PARAMETROS(
    IDPARAMETRO INT IDENTITY(1,1) PRIMARY KEY,
    NOMBREPARAMETRO VARCHAR(200),
    VALORPARAMETRO VARCHAR(10),
    TIPOPARAMETRO VARCHAR(50)
);


--- Eliminar la tabla GENERALTARJETA si ya existe ---
IF OBJECT_ID('GENERALTARJETA', 'U') IS NOT NULL
BEGIN
    DROP TABLE GENERALTARJETA;
END

--- Creación la tabla GENERALTARJETA ---
CREATE TABLE GENERALTARJETA(
    NUMEROTARJETA VARCHAR(16) PRIMARY KEY,
    NOMBREASOCIADO VARCHAR(300),
    FECHAVENCIMIENTO VARCHAR(7),
	CVV INT,
    LIMITE MONEY,
    SALDOACTUAL MONEY,
    SALDODISPONIBLE MONEY,
    FECHACORTE DATE
);

--- Eliminar la tabla COMPRASTARJETA si ya existe ---
IF OBJECT_ID('COMPRASTARJETA', 'U') IS NOT NULL
BEGIN
    DROP TABLE COMPRASTARJETA;
END

--- Creación la tabla COMPRASTARJETA ---
CREATE TABLE COMPRASTARJETA(
    IDCOMPRA INT IDENTITY(1,1) PRIMARY KEY,
    NUMEROTARJETA VARCHAR(16),
    FECHACOMPRA DATE,
    MES INT,
    ANIO INT,
    DESCRIPCION VARCHAR(300),
    MONTO MONEY,
    FOREIGN KEY (NUMEROTARJETA) REFERENCES GENERALTARJETA(NUMEROTARJETA)
);

--- Eliminar la tabla PAGOSTARJETA si ya existe ---
IF OBJECT_ID('PAGOSTARJETA', 'U') IS NOT NULL
BEGIN
    DROP TABLE PAGOSTARJETA;
END

--- Creación la tabla PAGOSTARJETA ---
CREATE TABLE PAGOSTARJETA(
    IDPAGO INT IDENTITY(1,1) PRIMARY KEY,
    NUMEROTARJETA VARCHAR(16),
    FECHAPAGO DATE,
    MES INT,
    ANIO INT,
    DESCRIPCION VARCHAR(200),
    MONTOPAGO MONEY,
    FOREIGN KEY (NUMEROTARJETA) REFERENCES GENERALTARJETA(NUMEROTARJETA)
);

---------------------------------------------------------------
--- CREACIÓN DE PROCEDIMIENTOS ALMACENADOS PARA LAS TABLAS  ---
---------------------------------------------------------------

--- CONSULTA DE HISTORIAL DE TRANSACCIONES ---
CREATE PROCEDURE sp_GetHistorialTarjeta
    @numeroTarjeta VARCHAR(16),
    @mes INT,
    @anio INT
AS
BEGIN
    SELECT 
        IDPAGO AS IDTRANSACCION,
        NUMEROTARJETA,
        FECHAPAGO AS FECHATRANSACCION,
        MES,
        ANIO,
        DESCRIPCION,
        MONTOPAGO AS MONTO,
        'P' AS TIPOTRANSACT
    FROM 
        PAGOSTARJETA
    WHERE 
        NUMEROTARJETA = @numeroTarjeta
        AND MES = @mes
        AND ANIO = @anio

    UNION

    SELECT 
        IDCOMPRA AS IDTRANSACCION,
        NUMEROTARJETA,
        FECHACOMPRA AS FECHATRANSACCION,
        MES,
        ANIO,
        DESCRIPCION,
        MONTO,
        'C' AS TIPOTRANSACT
    FROM 
        COMPRASTARJETA
    WHERE 
        NUMEROTARJETA = @numeroTarjeta
        AND MES = @mes
        AND ANIO = @anio

    ORDER BY 
        FECHATRANSACCION DESC;
END;

-- EXEC sp_GetHistorialTarjeta '4545252332120011', 9, 2024;
--------------------------------------------------------------------------

--- CONSULTA EL HISTORIAL DE COMPRAS ---

CREATE PROCEDURE sp_GetHistorialCompras
    @numeroTarjeta VARCHAR(16),
    @mes INT,
    @anio INT
AS
BEGIN
	SELECT 
        IDCOMPRA AS IDTRANSACCION,
        NUMEROTARJETA,
        FECHACOMPRA AS FECHATRANSACCION,
        MES,
        ANIO,
        DESCRIPCION,
        MONTO,
        'C' AS TIPOTRANSACT
    FROM 
        COMPRASTARJETA
    WHERE 
        NUMEROTARJETA = @numeroTarjeta
        AND MES = @mes
        AND ANIO = @anio

    ORDER BY 
        FECHATRANSACCION DESC;
END;

EXEC sp_GetHistorialCompras '4545252332120011', 9, 2024;

--------------------------------------------------------------------------

--- CONSULTA EL HISTORIAL DE PAGOS ---

CREATE PROCEDURE sp_GetHistorialPagos
    @numeroTarjeta VARCHAR(16),
    @mes INT,
    @anio INT
AS
BEGIN
	 SELECT 
        IDPAGO AS IDTRANSACCION,
        NUMEROTARJETA,
        FECHAPAGO AS FECHATRANSACCION,
        MES,
        ANIO,
        DESCRIPCION,
        MONTOPAGO AS MONTO,
        'P' AS TIPOTRANSACT
    FROM 
        PAGOSTARJETA
    WHERE 
        NUMEROTARJETA = @numeroTarjeta
        AND MES = @mes
        AND ANIO = @anio

    ORDER BY 
        FECHATRANSACCION DESC;
END;

EXEC sp_GetHistorialPagos '4545252332120011', 9, 2024;

--------------------------------------------------------------------------

--- CONSULTA LA INFORMACION DE LA TARJETA ---

CREATE PROCEDURE sp_ObtenerDatosTarjeta
    @NumeroTarjeta VARCHAR(16)
AS
BEGIN
    DECLARE @InteresConfigurableMinimo DECIMAL(5, 2);
    DECLARE @InteresConfigurable DECIMAL(5, 2);
    SELECT @InteresConfigurableMinimo = CAST(VALORPARAMETRO AS DECIMAL(5, 2)) / 100
    FROM PARAMETROS
    WHERE NOMBREPARAMETRO = 'MIN INTERES CONF';
    SELECT @InteresConfigurable = CAST(VALORPARAMETRO AS DECIMAL(5, 2)) / 100
    FROM PARAMETROS
    WHERE NOMBREPARAMETRO = 'INTERES CONF';
    SELECT 
        gt.NUMEROTARJETA,
        gt.NOMBREASOCIADO,
        gt.FECHAVENCIMIENTO,
        gt.CVV,
        gt.LIMITE,
        gt.SALDOACTUAL,
        gt.SALDODISPONIBLE,
        gt.FECHACORTE,
        @InteresConfigurableMinimo AS InteresConfigurableMinimo, -- Interés configurable mínimo
        @InteresConfigurable AS InteresConfigurable, -- Interés configurable
        (gt.SALDOACTUAL * @InteresConfigurable) AS InteresBonificable, -- Interés bonificable
        (gt.SALDOACTUAL * @InteresConfigurableMinimo) AS CuotaMinimaPagar, -- Cuota mínima a pagar
        (gt.SALDOACTUAL + (gt.SALDOACTUAL * @InteresConfigurable)) AS MontoTotalContado -- Monto total de contado con intereses
    FROM GENERALTARJETA gt
    WHERE gt.NUMEROTARJETA = @NumeroTarjeta;
END;

-- EXEC sp_ObtenerDatosTarjeta '4545252332120011'

--------------------------------------------------------------------------

--- CREACIÓN DE UN NUEVO PAGO ---

CREATE PROCEDURE sp_CrearPago
    @numeroTarjeta VARCHAR(16),
    @fechaPago DATE,
    @descripcion VARCHAR(200),
    @monto MONEY
AS
BEGIN
    DECLARE @saldoActual MONEY;
    DECLARE @saldoDisponible MONEY;

    BEGIN TRANSACTION;

    SELECT @saldoActual = SALDOACTUAL, @saldoDisponible = SALDODISPONIBLE
    FROM GENERALTARJETA
    WHERE NUMEROTARJETA = @numeroTarjeta;

    IF @saldoActual < @monto
    BEGIN
        ROLLBACK TRANSACTION;
        RAISERROR ('El monto del pago excede el saldo actual.', 16, 1);
        RETURN;
    END

    UPDATE GENERALTARJETA
    SET
        SALDODISPONIBLE = SALDODISPONIBLE + @monto,
        SALDOACTUAL = SALDOACTUAL - @monto
    WHERE NUMEROTARJETA = @numeroTarjeta;

    INSERT INTO PAGOSTARJETA (NUMEROTARJETA, FECHAPAGO, MES, ANIO, DESCRIPCION, MONTOPAGO)
    VALUES (
        @numeroTarjeta,
        @fechaPago,
        MONTH(@fechaPago),
        YEAR(@fechaPago),
        @descripcion,
        @monto
    );

    COMMIT TRANSACTION;
END;

-- EXEC sp_CrearPago '4545252332120011', '2024-09-05', 'Pago de tarjeta', 100;
--------------------------------------------------------------------------

--- CREACIÓN DE UNA NUEVA COMPRA --- 

CREATE PROCEDURE sp_CrearCompra
    @numeroTarjeta VARCHAR(16),
    @fechaCompra DATE,
    @descripcion VARCHAR(300),
    @monto MONEY
AS
BEGIN
    DECLARE @saldoActual MONEY;
    DECLARE @saldoDisponible MONEY;

    BEGIN TRANSACTION;

    SELECT @saldoActual = SALDOACTUAL, @saldoDisponible = SALDODISPONIBLE
    FROM GENERALTARJETA
    WHERE NUMEROTARJETA = @numeroTarjeta;

    IF @monto > @saldoDisponible
    BEGIN
        ROLLBACK TRANSACTION;
        RAISERROR ('El monto de la compra excede el saldo disponible.', 16, 1);
        RETURN;
    END

    UPDATE GENERALTARJETA
    SET
        SALDODISPONIBLE = SALDODISPONIBLE - @monto,
        SALDOACTUAL = SALDOACTUAL + @monto
    WHERE NUMEROTARJETA = @numeroTarjeta;

    INSERT INTO COMPRASTARJETA (NUMEROTARJETA, FECHACOMPRA, MES, ANIO, DESCRIPCION, MONTO)
    VALUES (
        @numeroTarjeta,
        @fechaCompra,
        MONTH(@fechaCompra),
        YEAR(@fechaCompra),
        @descripcion,
        @monto
    );

    COMMIT TRANSACTION;
END;


---------------------------------------------------------------
---      INSERSIÓN DE DATOS NECESARIOS PARA LAS TABLAS      ---
---------------------------------------------------------------
INSERT INTO PARAMETROS VALUES(2, 'MIN INTERES CONF', '2', 'PORCENTAJE');
INSERT INTO PARAMETROS VALUES(3, 'INTERES CONF', '25', 'PORCENTAJE');

INSERT INTO GENERALTARJETA 
(NUMEROTARJETA, NOMBREASOCIADO, FECHAVENCIMIENTO, CVV, LIMITE, SALDOACTUAL, SALDODISPONIBLE, FECHACORTE)
VALUES 
('4545252332120011', 'ALEXIS BARAHONA', '09/2026', 545, 1900, 1500, 400, '2024-09-14');

INSERT INTO COMPRASTARJETA (IDCOMPRA, NUMEROTARJETA, FECHACOMPRA, MES, ANIO, DESCRIPCION, MONTO)
VALUES 
(1, '4545252332120011', '2024-09-05', 9, 2024, 'Compra en supermercado', 150.75),
(2, '4545252332120011', '2024-09-12', 9, 2024, 'Compra en tienda de ropa', 320.50),
(3, '4545252332120011', '2024-09-20', 9, 2024, 'Compra de electrónicos', 650.00);


INSERT INTO PAGOSTARJETA (IDPAGO, NUMEROTARJETA, FECHAPAGO, MES, ANIO, DESCRIPCION, MONTOPAGO)
VALUES 
(1, '4545252332120011', '2024-09-07', 9, 2024, 'Pago parcial de tarjeta', 200.00),
(2, '4545252332120011', '2024-09-15', 9, 2024, 'Pago total de tarjeta', 500.00),
(3, '4545252332120011', '2024-09-22', 9, 2024, 'Pago de intereses', 50.00);
