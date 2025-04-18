# Project Structure
-----
## Backend
```
Todolist_Backend/
├── Controllers/
│   ├── BaseApiController.cs
│   ├── AccountController.cs
│   ├── LoginController.cs
│   ├── RegisterController.cs
│   ├── ResetController.cs
│   └── TodoController.cs
│
├── Models/
│   ├── DTOs/
│   │   ├── Account/
│   │   │   └── AccountDTO.cs
│   │   ├── Login/
│   │   │   └── LoginDTO.cs
│   │   ├── Register/
│   │   │   ├── EmailDTO.cs
│   │   │   └── RegisterDTO.cs
│   │   ├── Reset/
│   │   │   ├── EmailDTO.cs
│   │   │   └── ResetDTO.cs
│   │   └── Todo/
│   │       ├── EditStatusDTO.cs
│   │       ├── TodoDTO.cs
│   │       ├── TodoEditDTO.cs
│   │       └── UpdateCompleteDTO.cs
│   │
│   ├── Entities/
│   │   ├── EmailLog.cs
│   │   ├── ResetToken.cs
│   │   ├── Todo.cs
│   │   └── User.cs
│   │
│   └── TodolistDbContext.cs
│
├── Services/
│   ├── Interfaces/
│   │   ├── Account/
│   │   │   ├── IDeleteAccountService.cs
│   │   │   ├── IGetAccountService.cs
│   │   │   └── IUpdateAccountService.cs
│   │   ├── Email/
│   │   │   ├── IEmailLogger.cs
│   │   │   ├── IEmailSender.cs
│   │   │   └── IEmailService.cs
│   │   ├── Login/
│   │   │   └── ILoginService.cs
│   │   ├── Register/
│   │   │   └── IRegisterService.cs
│   │   ├── Reset/
│   │   │   └── IResetPasswordService.cs
│   │   ├── Todo/
│   │   │   ├── ICreateTodoService.cs
│   │   │   ├── IDeleteTodoService.cs
│   │   │   ├── IEditTodoService.cs
│   │   │   ├── IGetEditService.cs
│   │   │   ├── IGetTodoService.cs
│   │   │   └── IUpdateCompleteService.cs
│   │   ├── Token/
│   │   │   └── IJwtTokenService.cs
│   │   └── VerifyCode/
│   │       ├── ISendRegisterVerifyService.cs
│   │       ├── ISendResetVerifyService.cs
│   │       └── IVerificationCodeService.cs
│   │
│   ├── Account/
│   │   ├── DeleteAccountService.cs
│   │   ├── GetAccountService.cs
│   │   └── UpdateAccountService.cs
│   ├── Email/
│   │   ├── EmailLogger.cs
│   │   ├── EmailSender.cs
│   │   └── EmailService.cs
│   ├── Login/
│   │   └── LoginService.cs
│   ├── Register/
│   │   └── RegisterService.cs
│   ├── Reset/
│   │   └── ResetPasswordService.cs
│   ├── Todo/
│   │   ├── CreateTodoService.cs
│   │   ├── DeleteTodoService.cs
│   │   ├── EditTodoService.cs
│   │   ├── GetEditService.cs
│   │   ├── GetTodoService.cs
│   │   └── UpdateCompleteService.cs
│   ├── Token/
│   │   └── JwtTokenService.cs
│   └── VerifyCode/
│       ├── SendRegisterVerifyService.cs
│       ├── SendResetVerifyService.cs
│       └── VerificationCodeService.cs
│
├── Settings/
│   └── MailjetSettings.cs
│
├── Program.cs
└── appsettings.json
```
## Frontend
```
Todolist_Frontend/
├── src/
│   ├── api/
│   │   ├── account.js
│   │   ├── login.js
│   │   ├── register.js
│   │   ├── reset.js
│   │   └── todo.js
│   │
│   ├── assets/
│   │   └── tailwind.css
│   │
│   ├── components/
│   │   ├── shared/
│   │   │   ├── Footer.vue
│   │   │   └── Navbar.vue
│   │   │
│   │   ├── todo/
│   │   │   ├── Pagination.vue
│   │   │   ├── TodoFilterBar.vue
│   │   │   └── TodoTable.vue
│   │   │
│   │   ├── AccountForm.vue
│   │   ├── AddTodoForm.vue
│   │   ├── EditTodoForm.vue
│   │   ├── LoginForm.vue
│   │   ├── RegisterForm.vue
│   │   ├── ResetForm.vue
│   │   └── VerificationForm.vue
│   │
│   ├── router/
│   │   └── index.js
│   │
│   ├── service/
│   │   └── errorService.js
│   │
│   ├── store/
│   │   └── index.js
│   │
│   ├── views/
│   │   ├── todo/
│   │   │   ├── AddTodoView.vue
│   │   │   ├── EditTodoView.vue
│   │   │   └── TodoView.vue
│   │   │
│   │   ├── AccountView.vue
│   │   ├── LoginView.vue
│   │   ├── RegisterView.vue
│   │   └── ResetView.vue
│   │
│   ├── App.vue
│   └── main.js
│
├── package.json
└── tailwind.config.js
```
