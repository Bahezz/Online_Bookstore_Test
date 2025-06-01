# 📚 Online Bookstore Backend API

This project is a fully functional backend for an **Online Bookstore**, built with **ASP.NET Core Web API**, using **Entity Framework Core**, **PostgreSQL** as the database, and **JWT-based token authentication**.

## ✅ Features

### 👤 User Authentication

-   Register, Login, Logout functionality.
    
-   Password hashing with ASP.NET Core Identity.
    
-   Secure JWT token generation and validation.
    

### 📚 Book Management

-   CRUD APIs to add, edit, delete, and view books.
    
-   Search and filter books by title, author, or genre.
    
-   Track book availability.
    

### 🛒 Shopping Cart

-   Add books to cart (only if available).
    
-   View and remove cart items.
    
-   Checkout functionality creates an order and clears the cart.
    

### 📦 Order Processing

-   Create orders during checkout.
    
-   Orders contain multiple order items with pricing.
    
-   Tracks total amount, shipping address, and timestamps.
    
-   Book availability is updated after order.
    

### 🔒 Security

-   Input validation and model state checking.
    
-   Prevents SQL injection and XSS.
    
-   Sets security headers (CSP, etc.).
    
-   Uses middleware for authentication and request logging.
    

## 🧰 Tech Stack

-   ASP.NET Core Web API
    
-   Entity Framework Core
    
-   PostgreSQL
    
-   AutoMapper
    
-   JWT Authentication
    
-   Swagger (for API testing)
    

## 🔧 Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/OnlineBookstoreAPI.git
cd OnlineBookstoreAPI

```

### 2. Update `appsettings.json`

Open the `appsettings.json` file and add your PostgreSQL connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=5432;Database=BookstoreTestDb;Username=your_username;Password=your_password"
},


```

### 3. Run EF Core Migrations

Make sure PostgreSQL is running locally.

```bash
dotnet ef database update

```

### 4. Run the Project

```bash
dotnet run

```


    

## ✍️ Author

Developed by bahez maghdid hamad ameen. Contributions and feedback welcome!
