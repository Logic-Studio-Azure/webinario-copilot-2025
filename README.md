# Product Requirements Document (PRD)
## Aplicación de Registro de Asistentes para Webinario en Vivo

### 1. Objetivo
Desarrollar una aplicación web sencilla en .NET Blazor para registrar asistentes a un webinario en vivo, permitiendo a los organizadores visualizar y exportar la lista de participantes. Los datos se simularán usando un archivo JSON local, sin backend.

### 2. Funcionalidades Principales
- **Registro de asistentes:** Formulario simple con campos de nombre, empresa, teléfono y correo electrónico.
- **Listado de asistentes:** Visualización en tiempo real de los registros, consultando y actualizando un archivo JSON simulado.
- **Interfaz minimalista:** Diseño claro y responsivo, fácil de usar en cualquier dispositivo.

### 3. Requisitos Técnicos
- **Framework:** .NET Blazor (WebAssembly).
- **Persistencia:** Simulación de almacenamiento y consulta de datos usando `localStorage` (JSON en cliente).
- **Despliegue:** Puede ejecutarse localmente o como Static Web App en Azure si se requiere acceso externo.
- **Sin autenticación:** No se requiere login para asistentes ni organizadores.

### 4. Usuarios
- **Organizadores:** Acceso a la lista y exportación.
- **Asistentes:** Acceso solo al formulario de registro.

### 5. Restricciones
- Cumplimiento básico con protección de datos (mostrar aviso de privacidad).
- Interfaz debe funcionar correctamente en dispositivos móviles y escritorio.

### 6. Métricas de Éxito
- Número de asistentes registrados.
- Facilidad de uso (feedback de los organizadores).
- Funcionamiento estable durante el webinario.

---
## Implementación Realizada
Proyecto generado en `src/WebinarRegistration` como Blazor WebAssembly standalone.

### Estructura Clave
```
src/WebinarRegistration/
  Models/Attendee.cs
  Services/IAttendeeService.cs
  Services/LocalStorageAttendeeService.cs
  Components/RegistrationForm.razor
  Components/AttendeeList.razor
  Pages/Index.razor (Registro)
  Pages/Organizer.razor (Panel organizador)
  wwwroot/js/storage.js (localStorage + descarga)
```

### Persistencia
Se almacena la lista bajo la clave `attendees:v1` en `localStorage`. La exportación genera archivos CSV o JSON mediante JS interop.

### Validación
- Nombre y Email requeridos.
- Email con formato válido.
- Longitudes máximas controladas.

### Exportación
Botones en el panel organizador: CSV y JSON.

### Privacidad
Aviso textual incluido debajo del formulario.

### Responsividad
Uso de grid CSS y ocultación condicional de columna Teléfono en pantallas pequeñas.

---
## Cómo Ejecutar Localmente
Prerequisitos: .NET 8 SDK instalado.

```pwsh
cd src/WebinarRegistration
dotnet run
```
Abrir navegador en la URL mostrada (por defecto `https://localhost:****`).

Rutas:
- `/` Registro de asistentes.
- `/organizer` Panel con lista y exportación.

> Nota: Los datos se almacenan en el navegador actual. Limpiar cache/localStorage reinicia la lista.

---
## Posible Despliegue en Azure Static Web Apps
1. Ejecutar `dotnet publish -c Release`.
2. Carpeta de salida: `bin/Release/net8.0/publish/wwwroot`.
3. Configurar GitHub Action de Static Web Apps apuntando a esa carpeta.

---
## Futuras Mejoras
- Filtro y búsqueda de asistentes.
- Validación de duplicados por email.
- Exportación directa a Excel (XLSX) usando librería cliente.
- Internacionalización (i18n).
- Persistencia real vía API / Azure Functions.
- Protección simple con clave / token para ruta `/organizer`.

---
## Checklist de Requisitos
| Requisito | Estado |
|-----------|--------|
| Registro asistentes | Implementado |
| Listado en tiempo real | Implementado (recarga manual/rápida) |
| Exportación CSV/JSON | Implementado |
| Aviso privacidad | Implementado |
| Diseño responsivo | Implementado |
| Sin backend real | Cumplido |

---
## Licencia
Ver archivo `LICENSE`.
