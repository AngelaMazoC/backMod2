# firstback
Proyecto Backend - segundo semestre - Nodo EAFIT.

Api User
La API incluye operaciones CRUD (Crear, Leer, Actualizar, Eliminar) para la entidad User, y utiliza Entity Framework Core para interactuar con la base de datos.

Características principales
- CRUD de Usuarios: Crear, leer, actualizar y eliminar usuarios.
- DTOs (Data Transfer Objects): Uso de DTOs para transferir datos entre el cliente y el servidor.
- Swagger: Documentación automática de la API mediante Swagger.
- PostgreSQL: Base de datos relacional para almacenar la información de los usuarios.

La ESTRUCTURA está organizado de la siguiente manera:

- Models/: Contiene las entidades del dominio, como User.
- DTOs/: Contiene los objetos de transferencia de datos (DTOs), como UserDTO.
- Services/: Contiene la lógica de negocio, como UserService.
- Controllers/: Contiene los controladores de la API, como UserController.
- Data/: Contiene el contexto de la base de datos (ApplicationDbContext).
