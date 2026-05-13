# Contexto real del proyecto

Estoy desarrollando una nueva aplicación de escritorio en C# Windows Forms para integración biométrica con ZKTeco ZK4500.

Tengo una aplicación antigua funcional llamada `RegistroHuella.cs`, la cual ya conecta correctamente con:

- lector ZK4500
- MongoDB
- carga empleados
- registra huellas

Pero ese código está desordenado y toda la lógica está en un solo formulario.

Quiero crear una nueva versión mejor estructurada reutilizando la lógica existente.

## Código base existente

El archivo antiguo usa:

### Librerías:

- libzkfpcsharp
- MongoDB.Driver
- Windows Forms

### Conexión actual a MongoDB

Usa:

- base de datos: opticentro3
- colección: users

Consulta usuarios con:

- ap_paterno
- ap_materno
- nombre
- huella

### Conexión actual al lector

Usa:

- zkfp.Initialize()
- zkfp2.OpenDevice(0)
- zkfp2.DBInit()
- zkfp2.AcquireFingerprint()

## Objetivo

Ayúdame a crear una nueva aplicación limpia, separando responsabilidades.

NO quiero copiar el formulario antiguo completo.

Quiero reutilizar únicamente la lógica de:

- conexión al dispositivo
- carga de empleados
- preparación de captura

## Arquitectura requerida

Usa esta estructura:

/Presentation
    FrmBiometricEnrollment.cs

/Application
    Services
        BiometricService.cs
        EmployeeService.cs

/Domain
    Models
        Employee.cs

/Infrastructure
    Devices
        ZK4500DeviceService.cs
    Data
        MongoConnection.cs
        EmployeeRepository.cs

/Common
    Helpers
        LoggerHelper.cs

## Instrucciones

Analiza el código antiguo y ayúdame a:

1. identificar qué métodos reutilizar
2. separar responsabilidades
3. crear nueva solución limpia
4. generar archivos paso a paso
5. comenzar por la conexión del lector ZK4500

Empieza explicando:

- qué partes del archivo RegistroHuella.cs reutilizar
- qué partes descartar
- primer archivo a crear