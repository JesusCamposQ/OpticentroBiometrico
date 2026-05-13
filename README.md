# OpticentroBiometrico

Aplicación de escritorio para el registro biométrico de empleados de Opticentro. Permite conectar el lector de huellas ZK4500 y consultar la lista de empleados desde la base de datos MongoDB.

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

La colección utilizada es `users`. Cada documento debe tener la siguiente estructura:

```json
{
  "_id": "ObjectId",
  "nombre": "Juan",
  "ap_paterno": "Pérez",
  "ap_materno": "López",
  "ci": "12345678",
  "isActive": true
}
```

Solo se muestran en la aplicación los empleados donde `isActive` sea `true`.

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

1. **Conectar dispositivo** — Pulsar el botón "Conectar dispositivo". Se mostrará un mensaje de éxito o error.
2. **Cargar empleados** — Pulsar el botón "Cargar empleados". La lista se cargará desde MongoDB.
3. **Seleccionar empleado** — Hacer clic sobre un empleado en la tabla para seleccionarlo.
4. El botón "Iniciar enrolamiento" se habilitará al seleccionar un empleado (flujo preparado para futuras historias).

## Logs

Los errores de conexión y operación se registran automáticamente en el archivo:

```
log.txt
```

El archivo se genera en el mismo directorio del ejecutable y se actualiza de forma acumulativa con fecha y hora en cada entrada.

## Estructura del proyecto

```
OpticentroBiometrico/
├── Application/
│   └── Services/
│       └── EmployeeService.cs       # Capa de servicios
├── Common/
│   └── Logger.cs                    # Registro de errores
├── Domain/
│   └── Models/
│       └── Employee.cs              # Modelo de empleado
├── Intrastructure/
│   ├── Data/
│   │   ├── MongoConnection.cs       # Conexión a MongoDB
│   │   └── EmployeeRepository.cs   # Acceso a datos
│   └── Devices/
│       └── ZK4500DeviceService.cs  # Integración con ZK4500
├── Libraries/
│   ├── libzkfpcsharp.dll            # SDK ZKTeco
│   ├── MongoDB.Bson.dll
│   ├── MongoDB.Driver.dll
│   └── MongoDB.Driver.Core.dll
├── FrmBiometricEnrollment.cs        # Formulario principal
├── App.config                       # Configuración de conexión
└── Program.cs                       # Punto de entrada
```

## Fuera de alcance (versión actual)

- Captura y registro de huella digital
- Validación biométrica
- Almacenamiento de templates de huella
- Sincronización con backend
