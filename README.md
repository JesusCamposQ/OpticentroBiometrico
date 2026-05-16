# OpticentroBiometrico

Aplicación de escritorio para el registro biométrico de empleados de Opticentro. Permite conectar el lector de huellas ZK4500, seleccionar un empleado desde la base de datos MongoDB y capturar su huella dactilar generando un template biométrico que queda almacenado asociado al empleado.

## Requisitos previos

| Requisito | Versión |
|-----------|---------|
| Windows | 10 o superior |
| Visual Studio | 2019 o superior |
| .NET Framework | 4.8 |
| MongoDB | 4.x o superior |

## Instalación de drivers ZK4500

1. Conectar el dispositivo ZK4500 por USB antes de instalar los drivers.
2. Descargar los drivers oficiales desde el sitio de ZKTeco: [https://www.zkteco.com/](https://www.zkteco.com/) (sección de soporte > ZK4500).
3. Ejecutar el instalador como **Administrador**.
4. Reiniciar el equipo al finalizar la instalación.
5. Verificar en el **Administrador de dispositivos** que el ZK4500 aparece sin errores bajo la categoría "Dispositivos de imagen" o similar.

> Las DLLs del SDK (`libzkfpcsharp.dll`) ya están incluidas en la carpeta `Libraries/` del proyecto y no requieren instalación adicional.

## Configuración

Editar el archivo `OpticentroBiometrico/App.config` con los datos de conexión a MongoDB:

```xml
<appSettings>
    <add key="MongoConnectionString" value="mongodb://localhost:27017" />
    <add key="MongoDatabaseName" value="nombre_de_la_bd" />
</appSettings>
```

| Clave | Descripción | Ejemplo |
|-------|-------------|---------|
| `MongoConnectionString` | Cadena de conexión a MongoDB | `mongodb://localhost:27017` |
| `MongoDatabaseName` | Nombre de la base de datos de Opticentro | `opticentro_db` |

### Estructura esperada en MongoDB

La colección utilizada es `users`. Cada documento debe tener la siguiente estructura mínima:

```json
{
  "_id": "ObjectId",
  "nombre": "Juan",
  "ap_paterno": "Pérez",
  "ap_materno": "López",
  "isActive": true,
  "huella": null
}
```

- Solo se muestran empleados donde `isActive` sea `true`.
- El campo `huella` se crea o actualiza al registrar la huella del empleado (base64).

## Compilación y ejecución

1. Abrir `OpticentroBiometrico.slnx` en Visual Studio.
2. Verificar que las referencias a las DLLs en `Libraries/` estén correctamente resueltas.
3. Compilar la solución (`Ctrl + Shift + B`).
4. Ejecutar la aplicación (`F5` o `Ctrl + F5`).

También se puede ejecutar directamente el binario compilado:

```
OpticentroBiometrico\bin\Debug\OpticentroBiometrico.exe
```

## Uso de la aplicación

### Flujo principal de enrolamiento

1. **Conectar dispositivo** — Pulsar "Conectar dispositivo". Se mostrará confirmación de éxito o el mensaje de error correspondiente.
2. **Cargar empleados** — Pulsar "Cargar empleados". La lista se carga desde MongoDB mostrando solo empleados activos.
3. **Seleccionar empleado** — Hacer clic sobre un empleado en la tabla. El botón "Iniciar enrolamiento" se habilita al seleccionar.
4. **Iniciar enrolamiento** — Pulsar "Iniciar enrolamiento". El proceso requiere **3 capturas consecutivas** del mismo dedo:
   - La barra de progreso avanza una posición por cada captura aceptada.
   - El indicador de estado guía al usuario en cada paso.
   - Cada captura es validada por calidad (umbral mínimo: 60%). Las huellas con baja calidad son rechazadas con aviso para reintentar.
   - Al completar las 3 capturas, el SDK genera un template fusionado (`DBMerge`) que se almacena en MongoDB en el campo `huella` del empleado.

### Mensajes de estado

| Mensaje | Significado |
|---|---|
| "Captura X de 3 — Coloque el dedo en el sensor..." | Esperando que el usuario apoye el dedo |
| "Calidad baja (X%) — Retire y vuelva a colocar el dedo..." | Captura rechazada por baja calidad |
| "Captura X aceptada (calidad: X%)" | Captura válida registrada |
| "Capture X OK — Retire el dedo..." | Pausa entre capturas |
| "Plantilla generada correctamente." | Merge exitoso, pendiente de guardado |
| "Huella capturada y guardada correctamente." | Proceso completado |
| "Error: el dispositivo parece haberse desconectado..." | Desconexión detectada durante la captura |
| "Captura X fallida. Tiempo agotado." | No se detectó huella en el tiempo máximo |

## Comportamiento ante errores del dispositivo

- Si el lector se **desconecta durante la captura**, el sistema detecta 5 errores consecutivos del SDK, aborta el proceso, notifica al usuario y registra el incidente en el log.
- Si el lector **no está conectado** al pulsar "Iniciar enrolamiento", se bloquea la operación con aviso inmediato.
- La liberación del dispositivo (`DBFree → CloseDevice → Terminate`) se ejecuta siempre al cerrar la aplicación, garantizando que el lector no quede bloqueado.

## Logs

Los errores de conexión, operación del SDK y almacenamiento se registran automáticamente en:

```
log.txt
```

El archivo se genera en el mismo directorio del ejecutable y se actualiza de forma acumulativa con fecha y hora en cada entrada. Incluye códigos de retorno del SDK para facilitar el diagnóstico.

## Estructura del proyecto

```
OpticentroBiometrico/
├── Application/
│   └── Services/
│       ├── EmployeeService.cs              # Carga y consulta de empleados
│       └── FingerprintEnrollmentService.cs # Captura, validación y generación de template
├── Common/
│   └── Logger.cs                           # Registro de errores en log.txt
├── Domain/
│   └── Models/
│       └── Employee.cs                     # Modelo de empleado
├── Intrastructure/
│   ├── Data/
│   │   ├── MongoConnection.cs              # Conexión a MongoDB
│   │   └── EmployeeRepository.cs          # Consulta de empleados
│   ├── Devices/
│   │   └── ZK4500DeviceService.cs         # Ciclo de vida del dispositivo ZK4500
│   └── Repositories/
│       └── FingerprintRepository.cs       # Almacenamiento de templates en MongoDB
├── Libraries/
│   ├── libzkfpcsharp.dll                   # SDK ZKTeco
│   ├── MongoDB.Bson.dll
│   ├── MongoDB.Driver.dll
│   └── MongoDB.Driver.Core.dll
├── FrmBiometricEnrollment.cs              # Formulario principal
├── App.config                             # Configuración de conexión
└── Program.cs                             # Punto de entrada
```

## Fuera de alcance (versión actual)

- Validación biométrica 1:N (comparar huella contra la base completa)
- Login biométrico
- Sincronización con otros dispositivos ZKTeco
- Registro de múltiples dedos por empleado
