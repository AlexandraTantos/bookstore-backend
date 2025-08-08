BookStore is a backend application designed to manage a virtual bookstore. It provides CRUD operations for books, authors, publishers, and users. The project was built using modern .NET technologies and follows clean architecture principles.
This project was started as part of a Summer Practice Program and is currently under development.

Technologies Used:
  - .NET 8 Web API
  - MongoDB (using MongoDB Atlas)
  - MediatR – for CQRS and decoupled business logic
  - AutoMapper – for object mapping
  - FluentValidation – for request validation
  - JWT Authentication – with refresh token support
  - Swagger/OpenAPI – for API documentation
  - xUnit and Moq - for unit testing

Features:
  - User registration and login using JWT with refresh token
  - Full CRUD operations for: Books, Authors, Publishers
  - Fluent validation for all requests
  - Global error handling middleware
  - Filtering support (by name, birth year, etc.)
  - Sorting (ascending/descending by various fields like FirstName, Title, Nationality, etc.)
  - Pagination for list endpoints (skip/take logic)
  - Unit tests to validate service logic
