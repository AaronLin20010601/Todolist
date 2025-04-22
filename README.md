# Todolist
First practice project of using .net core api and vue.js  

-----
### Backend:
.NET core api  
### Frontend:
vue.js  
### Database:
PostgreSQL on cloud database Supabase  

-----
## Additional packages added
### .NET Backend:
- BCrypt:For password hashing
- Mailjet:For mail sending
- JwtBearer:For JWT token verification
- Npgsql:For connection with PorstgreSQL
  
### Vue Frontend:
- Tailwindcss:For css styling
- Vuex:For data storage and sync
- Axios:For api request

-----
## Data model design
- User:User's basic data and infos.
  - Columns:Id, Username, Email, PasswordHash, CreatedAt.  
  
- Todo:Contents of Todolist.
  - Columns:Id, Title, Description, IsCompleted, CreatedAt, DueDate, UserId.  
  
- EmailLog:Email logs record.
  - Columns:Id, ToEmail, Subject, Body, SentAt, IsSuccess.
  
- ResetToken:Tokens using on register and reset password.
  - Columns:Id, Token, Email, CreatedAt, ExpirationDate, IsUsed.

-----
## Technical Architecture & Design
This project follows a clean architecture with clear separation of concerns between layers:
  
- Backend (ASP.NET Core API) is built with a modular structure using:
  - DTOs for input/output separation from database entities.
  - Entity Framework Core (Code First) for database access with migration support.
  - Service Layer with Interfaces to encapsulate business logic and promote testability and scalability.
  - Data Annotations & Fluent Validation for backend-side data validation.
  - JWT Token Authentication with middleware to protect routes and manage user sessions.
  - Stateless API Design following RESTful principles, ensuring consistent and scalable API behavior.
  
- Frontend (Vue.js) is structured with component-based design and:
  - Vue Router to manage client-side routing.
  - Vuex for central state management and token persistence across views.
  - Axios for asynchronous API communication with token-based headers.
  - Form Validation with Custom Error Handling for synchronized front-end and back-end validation feedback.
  - Component Modularization (e.g. TodoTable, TodoFilterBar, Pagination) for improved reusability and maintainability.
  
The backend and frontend are fully decoupled, enabling independent development, testing, and deployment.  
The application is developed using Supabase PostgreSQL as a managed cloud database service.

-----
## Project setup and compile on development
For backend part:  
1. Create a visual studio 2022 .net core web api project in Todolist folder.
2. When need to compile and test via cmd, enter the following commands:  
```sh
cd Todolist/Todolist_Backend/Todolist_Backend
```  
```sh
dotnet run
```  
  
For frontend part:  
1. Create a new vue default project by the following commands and steps:  
```sh
cd Todolist
```  
```sh
npm create vite@latest Todolist_Frontend
```  
Select framework as vue, choose variant as Offical Vue Starter.  
Select Router and ESLint.  
```sh
npm install
```  

3. When need to comiple and test via cmd, enter the following commands:  
```sh
cd Todolist/Todolist_Frontend
```  
```sh
npm run dev
```  

-----
## Features
1. User registration and login:
  - Users can register by providing an email, username, password and verification code. A verification code email will be sent via Mailjet.
  - Login with email and password, with JWT token generation for secure access.
  
2. Password reset:
  - Users can reset their password by providing email, then users can enter new password and verification code they get. A verification code email will be sent via Mailjet.
  
3. Todolist:
  - Users can view all their todos and filter them by completion status and duedate time.
  - Users can check off and complete their todos that have not expired yet.
  - Todos are paginated with 10 items per page. Users can navigate between pages using pagination buttons, and pagination updates dynamically based on the selected filter status.
  - Users can create new todos or edit existing ones.
  - Users can delete their todos if necessary.
  
4. Account:
  - Users can edit and delete their account.
