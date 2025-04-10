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
  
## Features
1. User registration and login:
  - Users can register by providing an email, username, password and verification code. A verification code email will be sent via Mailjet.
  - Login with email and password, with JWT token generation for secure access.
  
2. Password reset:
  - Users can reset their password by providing email, then users can enter new password and verification code they get. A verification code email will be sent via Mailjet.
  
3. Todolist:
  - Users can view all their todos and filter them by completion status and due date.
  - Users can create new todos or edit existing ones.
  - Users can delete their todos if necessary.
