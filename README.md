# Todolist

First practice project of using .net core api and vue.js
--
Backend:.NET core api  
Frontend:vue.js  
Database:PostgreSQL on cloud database Supabase  

Additional package added
--
.NET Backend:  
BCrypt for password hash  
Mailjet for mail sending  
JwtBearer for JWT token verify  
Npgsql for connection with PorstgreSQL  
  
Vue Frontend:  
Tailwindcss for css style  
Vuex for data storage and sync  
  
Datatable design
--
User:User's basic data and infos. Columns:Id, Username, Email, PasswordHash, CreatedAt.  
Todo:Contents of Todolist. Columns:Id, Title, Description, IsCompleted, CreatedAt, DueDate, UserId.  
EmailLog:Email logs record. Columns:Id, ToEmail, Subject, Body, SentAt, IsSuccess.  
ResetToken:Tokens using on register and reset password. Columns:Id, Token, Email, CreatedAt, ExpirationDate, IsUsed.  
