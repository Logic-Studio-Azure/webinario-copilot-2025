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