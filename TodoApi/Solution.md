### Architectural Decisions

- ### Chosen Architecture
- Layered Architecture (Controller → Service → Repository → DB) for Separation of Concerns
- Controllers handle HTTP requests
- Services contain business logic
- Repository/DbContext handles data access

- ### Scalability
- Easy to extend (e.g., add caching, logging, validation)
- Testability
- Services can be unit tested independently
- Cleaner code structure and easier debugging

- ### Global Exception Handling
- Cleaner code
- Less duplication
- Easier maintenance

- ### Dependency Injection (DI)
- Used built-in ASP.NET Core DI for loose coupling

- ### EF Core with Migrations
-Ensures database schema versioning and consistency

- ### SQLite Database
-Lightweight and easy for development/testing




### Future Improvements

- ### Security
- Add JWT Authentication & Authorization
- Role-based access control

- ### Logging & Monitoring
- Integrate logging
- Add centralized monitoring

- ### UI Layer
- Add frontend (React / Angular)

