# Control de Estacionamiento - Backend (C# / .NET)

A continuación se detallan los pasos para el **levantamiento del proyecto backend**:

1. **Configura la conexión a la base de datos**

   En el archivo `appsettings.json`, en la sección `ConnectionStrings`, puedes usar cualquiera de las dos formas de conexión a SQL Server:

   ```json
   "ConnectionStrings": {
       //"DefaultConnection": "Server=localhost;Database=EstacionamientoBD;Trusted_Connection=True;TrustServerCertificate=True;",
       "DefaultConnection": "Server=localhost;Database=EstacionamientoBD;User Id=sa;Password=admin;TrustServerCertificate=True;",
   },
   ```

2. **Instala Entity Framework Core CLI (dotnet-ef)**  
   Si no tienes instalado **dotnet-ef**, ejecuta el siguiente comando para instalarlo globalmente:

   ```bash
   dotnet tool install --global dotnet-ef
   ```

3. **Ejecuta la migración de la base de datos**  
   Este comando creará la base de datos en SQL Server con sus tablas y algunos datos iniciales:

   ```bash
   dotnet ef database update
   ```

4. **Si se genera algún error**, verifica que te encuentras en la carpeta donde está el archivo `.csproj` del proyecto.

5. **Si el error persiste**, cierra el proyecto y vuelve a abrirlo para que la instalación global de `dotnet-ef` sea reconocida correctamente.

6. **Levanta el proyecto**  
   Si todo está correctamente configurado, levanta el proyecto.
