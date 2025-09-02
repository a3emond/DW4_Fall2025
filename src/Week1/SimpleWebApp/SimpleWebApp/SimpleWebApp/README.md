# SimpleWebApp – Auth & User Management Demo

## Overview

This project is a minimal ASP.NET (Framework 4.7.2) server-side rendered app demonstrating authentication, authorization, and basic user management. It is built as an exercise project to show how to wire up controllers, services, repositories, and JWT-based authentication from scratch.

---

## Features

### Authentication

* Register with username, email, and password

    * Passwords hashed with BCrypt.
    * Registration prevented if email already exists.
* Login with email & password

    * Valid credentials issue a JWT stored in an HTTP-only cookie.
* Logout clears the cookie.
* Auto-redirect to `/login` when accessing protected areas without authentication.

### Authorization

* Role-based system:

    * Default role: `user`
    * Admin role can list & manage all users.
* JWT claims: user ID, username, role.
* Tokens expire after 1 hour.

### User Operations

* `/api/profile` → Returns logged-in user info.
* `/api/admin/users` → List all users (admin only).
* `/api/admin/delete` → Delete a user (admin only).
* `/api/user/update` → Update own account (admins can update any).

### Frontend

* Server-Side Rendered (`index.html`, `login.html`, `register.html`)
* Realtime validation (JS):

    * Email format
    * Password strength (length, upper/lowercase, number, special char)
    * Confirm password match
* Simple card-style layout with feedback messages.
* Auto-redirects to login if not authenticated.

---

## Tech Stack

* ASP.NET Framework 4.7.2
* C# for Controllers, Services, and Routing
* MSSQL database (`main_db`)
* BCrypt.Net for password hashing
* JWT (HMAC SHA-256) for authentication
* Vanilla JS + HTML + CSS for frontend
* No frameworks (React/Angular/etc.) — built with raw web standards

---

## Key Structure

```
/Controllers
  - AuthController.cs    (login/logout)
  - UserController.cs    (register/profile/user mgmt)

/Services
  - UserService.cs       (business logic)
  - AuthService.cs       (JWT handling)
  - ServiceResult.cs     (standardized API response DTO)

/Repositories
  - UserRepository.cs    (DB access layer)

/Views
  - index.html
  - login.html
  - register.html

/js
  - main.js              (frontend validation + API calls)

/css
  - style.css
```

---

## API Response Format

All API endpoints return a `ServiceResult`-style JSON:

```json
{
  "success": true,
  "message": "User registered successfully",
  "error": null,
  "token": null
}
```

---

## Learning Objectives

* Implementing custom routing without MVC framework.
* Using JWT authentication in ASP.NET.
* Applying role-based authorization.
* Structuring code into controllers, services, repositories.
* Adding frontend form validation with vanilla JS.

---

This app is not production-ready — it is designed for educational purposes to practice full-stack auth logic with a clear separation of concerns.
