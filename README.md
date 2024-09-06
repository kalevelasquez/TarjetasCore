
# TarjetasCore

  

## Descripción

  

**TarjetasCore** es una aplicación para la gestión de tarjetas de crédito, que incluye funcionalidades como el registro de compras, pagos, y la visualización del estado de cuenta. Está desarrollada con un backend en .NET 6 y un frontend en Next.js, utilizando una base de datos en SQL Server.

  

## Requisitos Previos

  

Para poder correr el sistema es necesario tener instalados los siguientes componentes:

  

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

- [Node.js](https://nodejs.org/) (para el frontend)


  

## Configuración del Proyecto

  

1.  **Clona el repositorio**

  

```bash

git clone https://github.com/tu-usuario/tarjetascore.git

cd tarjetascore

```

  

2.  **Configuración del backend**

  

Ve a la carpeta del proyecto backend

  

```bash
cd TarjetasCore.Service
```
  

Asegúrate de configurar la cadena de conexión en el archivo appsettings.json


```json
"ConnectionStrings": 
{
	"SqlServerConnection": "Data Source=DESKTOP-MNUHLC8;Initial Catalog=TARJETASCORE;Integrated Security=True;TrustServerCertificate=True;"
}
```
  

3.  **Configuración de base de datos**

  

Ve a la carpeta Extras, luego a TarjetasCore.DB. Ahí encontrarás un archivo llamado: TARJETASCORE. Ábrelo en SQL Server Management Studio y ejecútalo para crear la base de datos con sus respectivos datos iniciales.

  

4.  **Uso de Postman**

Ahora, ve a la carpeta Extras nuevamente, luego a TarjetasCore.TestPostman. Ahí encontrarás un archivo llamado: TarjetasCore.Service.postman_collection. Importa dentro de Postman y ahora podrás probar la API .NET.

  

5.  **Configuración del frontend**

Ahora ve a la carpeta raiz y entra a TarjetasCore.WebPages. Dentro está: tarjetaswebpage, entra y verás el proyecto frontend.

Una vez dentro, si tienes el menú contextual de VS Code, da clic derecho en cualquier lugar del explorador, luego Abrir con code...

Si no tienes el menú contextual, abre VS Code o cualquier editor y abre la carpeta tarjetaswebpage.

  

En VS Code, o en un CMD en la carpeta ejecuta los siguientes comandos:

  

```bash
npm install
```
  

Ahora ejecuta el frontend en dev

```bash
npm run dev
```
  

Si por algún motivo el frondend presenta problemas de conexión con la API, ve en el proyecto a src/pages/data y dentro está el archivo data.tsx. Aquí puedes cambiar el puerto de cada llamado a la API para que pueda correr en el puerto que confitura el sistema.

  

6.  **Errores al intentar correr el frontend**

  
Si presenta problemas para poder compilar, podría ser que necesita instalaciones adicionales.

En una consola escribe:

  

```bash
npm install xlsx jspdf
npm install jspdf jspdf-autotable
```
  

7.  **Problemas adicionales**

  

Si presentas problemas adicionales no dudes en escribirme a kalexis.velasquez@gmail.com
