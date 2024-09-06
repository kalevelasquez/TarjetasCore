import { Transacciones, ParametrosConfigurables, InformacionTarjeta, GenericResponse, ObjectResponse, ListResponse, Pago, Compra } from "./definitions";

export async function getInformacionTargeta(numeroTarjeta:string): Promise<ObjectResponse<InformacionTarjeta>> {
    const response = await fetch(`https://localhost:7161/api/v1/Tarjeta/GetInfoTarjeta?numeroTarjeta=${numeroTarjeta}`,{cache: 'no-cache'});
    const json = await response.json();

    return json;
}

export async function getHistorialTransacciones(numeroTarjeta:string, mes:number, anio:number): Promise<ObjectResponse<Transacciones>>{
    const response = await fetch(`https://localhost:7161/api/v1/Transacciones/GetHistorialTransacciones?numeroTarjeta=${numeroTarjeta}&mes=${mes}&anio=${anio}`,{cache: 'no-cache'});
    const json = await response.json();

    return json;
}

export async function getHistorialCompras(numeroTarjeta:string, mes:number, anio:number): Promise<ObjectResponse<Transacciones>>{
    const response = await fetch(`https://localhost:7161/api/v1/Transacciones/GetHistorialCompras?numeroTarjeta=${numeroTarjeta}&mes=${mes}&anio=${anio}`,{cache: 'no-cache'});
    const json = await response.json();

    return json;
}

export async function getHistorialPagos(numeroTarjeta:string, mes:number, anio:number): Promise<ObjectResponse<Transacciones>>{
    const response = await fetch(`https://localhost:7161/api/v1/Transacciones/GetHistorialPagos?numeroTarjeta=${numeroTarjeta}&mes=${mes}&anio=${anio}`,{cache: 'no-cache'});
    const json = await response.json();

    return json;
}

export async function crearCompra(compra: Compra): Promise<GenericResponse>{
    const body = JSON.stringify(compra);
    const response = await fetch(`https://localhost:7161/api/v1/Transacciones/CrearNuevaCompra`, {cache: 'no-cache', method: 'POST', body: body, headers: {'Content-Type':'application/json'}});
    const json = await response.json();

    return json;
}

export async function crearPago(pago: Pago): Promise<GenericResponse>{
    const body = JSON.stringify(pago);
    const response = await fetch(`https://localhost:7161/api/v1/Transacciones/CrearNuevoPago`, {cache: 'no-cache', method: 'POST', body: body, headers: {'Content-Type':'application/json'}});
    const json = await response.json();

    return json;
}