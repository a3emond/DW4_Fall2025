# Auth App – Feature List

## Core Features
- Serve static pages:
    - `index.html` (home page)
    - `login.html` (login form)
    - `register.html` (registration form)

- Routing for clean URLs:
    - `/` → index
    - `/login` → login page
    - `/register` → register page
    - `/api/auth/login` → login endpoint (POST)
    - `/api/auth/register` → registration endpoint (POST)

---

## Authentication
- User table in SQL Server (`Users`):
    - `Id` (PK, int, identity)
    - `Username` (unique, nvarchar)
    - `PasswordHash` (nvarchar)
    - `Role` (`user` or `admin`)

- Password security:
    - Store using **bcrypt** hashing
    - Verify with bcrypt on login

- JWT support:
    - Issue JWT token on successful login
    - Token includes `sub` (user id) and `role`
    - Configurable secret stored in environment file

- Role-based access:
    - Endpoints protected by role (`admin` / `user`)
    - Example: `/api/admin/*` → accessible only by `admin`

---

## Security
- CSRF protection:
    - CSRF token included in forms
    - Token validated on POST requests

- Environment configuration:
    - Use `appsettings.shared.json` (instead of `.env`)
    - Shared with teacher to run locally
    - Contains:
        - Database connection string
        - JWT secret

---

## Frontend Validation
- Replace basic HTML form validation with **JavaScript dynamic validation**
- Real-time feedback for:
    - **Email format** (using regex)
    - **Password strength** (length, uppercase, lowercase, digits, special characters)
- Validation runs before form submission, preventing invalid requests from hitting the backend

---

## Project Structure
- **Controllers**
    - `AuthController.cs`
- **Services**
    - `AuthService.cs` (login, register, JWT)
- **Repositories**
    - `UserRepository.cs` (SQL queries)
- **Models**
    - `User.cs`
- **Static files**
    - `wwwroot/` with `index.html`, `login.html`, `register.html`

---