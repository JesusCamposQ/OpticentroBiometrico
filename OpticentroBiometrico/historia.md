Desarrollo de aplicación de escritorio para conexión con lector de huella ZK4500 y carga de usuarios desde BD


Tareas por hacer


Descripción

Como
Administrador del sistema de Opticentro

Quiero
Contar con una aplicación de escritorio que se conecte al lector de huellas ZK4500 y a la base de datos de Opticentro

Para
Poder obtener la lista de empleados y preparar el registro (enrolamiento) de sus huellas digitales

📌 Descripción
Se requiere desarrollar un aplicativo de escritorio utilizando Visual Studio (VB.NET  o C#) que permita:

Establecer conexión con el dispositivo biométrico ZK4500

Conectarse a la base de datos del sistema Opticentro

Consultar y mostrar la lista de empleados disponibles para enrolamiento

Preparar la estructura base para el registro de huellas (sin necesidad de guardar aún en esta historia)

El objetivo de esta historia es dejar lista la base técnica (infraestructura + conexión) para futuras historias relacionadas al enrolamiento biométrico.

🧱 Alcance Técnico
🔹 Aplicación de escritorio
Proyecto en Visual Studio

Lenguaje: C# (preferido) o VB.NET

Tipo: Windows Forms

🔹 Integración con ZK4500
Uso del SDK oficial de ZKTeco

Inicialización del dispositivo

Validación de conexión exitosa

🔹 Conexión a Base de Datos
Conexión a BD de Opticentro (MongoDB)

Implementar capa de acceso a datos

Consulta de empleados:

ID

Nombre completo

Documento (opcional)

Estado (activo/inactivo)

🔹 Interfaz mínima
Botón: "Conectar dispositivo"

Botón: "Cargar empleados"

Tabla/listado de empleados

🧪 Criterios de Aceptación
✔ Conexión al dispositivo
El sistema detecta correctamente el dispositivo ZK4500 conectado por USB

Se muestra mensaje de conexión exitosa o error

✔ Conexión a base de datos
El sistema se conecta correctamente a la BD de Opticentro

Manejo de errores en caso de fallo de conexión

✔ Listado de empleados
Se pueden consultar los empleados desde la BD

Se visualizan en una grilla/lista en pantalla

Solo se muestran empleados activos (si aplica)

✔ Base para enrolamiento
Se puede seleccionar un empleado de la lista

Se deja preparado el flujo para futura captura de huella

⚙️ Consideraciones Técnicas
Instalar drivers del ZK4500 en el equipo cliente

Incluir DLLs del SDK de ZKTeco en el proyecto

Arquitectura sugerida:

UI Layer (Forms)

Service Layer

Data Access Layer

Manejo de logs para errores de conexión

Configuración de conexión a BD mediante archivo (app.config)

🚫 Fuera de Alcance (para esta historia)
Registro de huella digital

Validación biométrica

Sincronización con backend

Almacenamiento de templates

🚀 Definición de Terminado (DoD)
Código funcional en repositorio

Aplicación ejecutable que:

Se conecta al ZK4500

Consulta empleados desde la BD

Muestra la lista en UI

Documentación básica de instalación (drivers + ejecución)