export interface ListResponse<T> {
    code: number;
    message: string;
    items: T[];
}

export interface ObjectResponse<T> {
    code: number;
    message: string;
    item: T
}

export interface GenericResponse {
    code: number;
    message: string;
}

export interface InformacionTarjeta{
    numeroTarjeta: string,
    nombreAsociado: string,
    fechaVencimiento: string,
    cvv: number,
    limite: number,
    saldoActual: number,
    saldoDisponible: number,
    fechaCorte: Date,
    interesConfigurableMinimo: number,
    interesConfigurable: number,
    interesBonificable: number,
    cuotaMinimaPagar: number,
    montoTotalContado: number
}

export interface ParametrosConfigurables{
    idParametro: number,
    nombreParametro: string,
    valorParametro: string,
    tipoParametro: string
}

export interface Transacciones{
    idTransaccion: number, 
    numeroTarjeta: string,
    fechaTransaccion: Date,
    mes: number,
    anio: number,
    descripcion: string,
    monto: number,
    tipoTransact: string
}

export interface Compra{
    numeroTarjeta: string, 
    fechaCompra: Date,
    descripcion: string,
    monto: number
}

export interface Pago{
    numeroTarjeta: string,
    fechaPago: Date,
    descripcion: string,
    montoPago: number
}

export interface HistorialTransaccionesResponse {
    item: Transacciones[];
  }