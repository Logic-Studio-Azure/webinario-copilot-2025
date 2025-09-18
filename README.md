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

## Ejecución del Proyecto

### Requisitos
- SDK .NET 9 instalado (`dotnet --version`).

### Ejecutar Localmente

```bash
cd WebinarRegistro
dotnet run
```
Abrir el navegador en la URL mostrada (por defecto `http://localhost:5000` o similar) y usar:
- Página principal: formulario de registro.
- `/organizador`: listado y exportación CSV.

### Estructura Clave
- `Models/Attendee.cs`: Modelo de asistente.
- `Services/AttendeeService.cs`: Lógica para almacenar y recuperar usando `localStorage` vía JSInterop.
- `wwwroot/js/attendeeStorage.js`: Wrapper JS para persistencia y exportación.
- `Pages/Home.razor`: Formulario de registro.
- `Pages/Organizador.razor`: Listado y exportación.
- `Shared/PrivacyNotice.razor`: Aviso de privacidad reutilizable.

### Exportar CSV
En la vista de organizador, pulsar "Exportar CSV". Se genera un archivo con encabezados y los datos actuales.

### Personalización / Limpieza de Datos
Abrir herramientas de desarrollador y limpiar `localStorage` (clave `webinar_attendees_v1`) si se desea reiniciar.

### Despliegue Sugerido
Puede publicarse como Static Web App (Azure) o en cualquier hosting estático ejecutando:
```bash
dotnet publish -c Release -o build
```
El contenido listo para servir estará en `build/wwwroot`.

---

## Próximas Mejoras (Opcional)
- Validaciones más estrictas (correo, longitud teléfono).
- Filtro / búsqueda en lista de asistentes.
- Indicadores visuales de auto-actualización.
- Soporte de importación desde CSV previo.