# Proyecto en .NET Core 9

## Información del autor
**Harold Rondon**  
Correo: [haroldcordero64@gmail.com](mailto:haroldcordero64@gmail.com)

## Descripción del Proyecto
Este proyecto es una API desarrollada en .NET Core 9 para la gestión de usuarios, roles, autenticación y autorización, con funcionalidades para exponer una lista de permisos disponible para el frontend.

---

## Requisitos

- .NET Core 9
- MySql 8 o mayor
- Configuración de variables de entorno:
    - `DB_CONNECTION_STRING`: Cadena de conexión a la base de datos SQL Server.
    - `JWT_SECRET_KEY`: Clave secreta para la generación de tokens JWT.

---

## Configuración Inicial

1. **Configurar Variables de Entorno**:
    - Establecer la variable `DB_CONNECTION_STRING` con la cadena de conexión al servidor de base de datos.
    - Establecer la variable `JWT_SECRET_KEY` con una clave secreta segura para la generación de JWT.

   Ejemplo en sistemas operativos basados en Unix:
   ```bash
   export DB_CONNECTION_STRING="Server=localhost;Database=MiBaseDeDatos;User Id=MiUsuario;Password=MiContraseña;"
   export JWT_SECRET_KEY="ClaveSecretaParaJWT"
   ```

   Ejemplo en Windows PowerShell:
   ```powershell
   $env:DB_CONNECTION_STRING="Server=localhost;Database=MiBaseDeDatos;User Id=MiUsuario;Password=MiContraseña;"
   $env:JWT_SECRET_KEY="ClaveSecretaParaJWT"
   ```

2. **Restaurar Dependencias**:
   Ejecuta el siguiente comando para restaurar las dependencias del proyecto:
   ```bash
   dotnet restore
   ```

3. **Actualizar Base de Datos**:
   Generar y aplicar las migraciones para crear las tablas necesarias, poblar roles y permisos, y configurar un usuario por defecto:
   ```bash
   dotnet ef database update
   ```

   **Detalles de los datos preconfigurados:**
    - Roles: Se poblarán automáticamente.
    - Usuario por defecto:
        - **Email**: `info@lilab.com`
        - **Contraseña**: `prueba`

---

## Ejecución

1. **Iniciar la API**:
   Usa el siguiente comando para iniciar la aplicación:
   ```bash
   dotnet run
   ```

2. **Probar la API**:
    - Accede al endpoint base en: `http://localhost:5000`
    - Utiliza las credenciales de prueba para autenticación.

---

## Migraciones

Para generar nuevas migraciones, sigue estos pasos:

1. **Agregar una Migración**:
   Ejecuta el siguiente comando:
   ```bash
   dotnet ef migrations add NombreDeLaMigracion
   ```

2. **Aplicar Migraciones**:
   Ejecuta el siguiente comando:
   ```bash
   dotnet ef database update
   ```

3. **Revertir una Migración (opcional)**:
   Si necesitas deshacer una migración específica:
   ```bash
   dotnet ef migrations remove
   ```

---

## Contacto
Para consultas adicionales, por favor contacta a: [haroldcordero64@gmail.com](mailto:haroldcordero64@gmail.com)
