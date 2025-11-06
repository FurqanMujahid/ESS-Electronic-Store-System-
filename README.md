# ESS - Electronic Store System

A comprehensive Electronic Store Management System built with ASP.NET MVC, Entity Framework, and ASP.NET Identity. This system provides a complete solution for managing an online electronics store with features for both customers and administrators.

## 👤 Author

**Muhammad Furqan Mujahid**

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Database Schema](#database-schema)
- [Key Components](#key-components)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

## 🎯 Overview

ESS (Electronic Store System) is a full-featured e-commerce platform designed specifically for managing an electronics store. The system provides:

- **Customer Features**: Product browsing, search, shopping cart, checkout, and account management
- **Admin Features**: Product management, category management, dashboard, and user administration
- **Security**: Role-based authentication, two-factor authentication, and secure password management

## ✨ Features

### Customer Features

1. **Product Browsing**
   - View all available products with pagination
   - Search products by name/description
   - Product details and images
   - Category-based filtering

2. **Shopping Cart**
   - Add products to cart
   - Update product quantities
   - Remove products from cart
   - Session-based cart management
   - Real-time cart updates

3. **Checkout System**
   - Checkout page with cart summary
   - Checkout details page
   - Shipping information management

4. **User Account Management**
   - User registration and login
   - Password reset functionality
   - Profile management
   - Two-factor authentication support
   - Phone number verification
   - External login providers support

### Admin Features

1. **Dashboard**
   - Admin control panel
   - System overview

2. **Product Management**
   - Add new products with images
   - Edit existing products
   - View all products
   - Product image upload and storage
   - Product categorization
   - Price and quantity management

3. **Category Management**
   - Create new categories
   - Edit categories
   - View all categories
   - Soft delete functionality (IsDelete flag)
   - Category activation/deactivation

4. **User Management**
   - View registered users
   - Manage user roles
   - User authentication management

## 🛠 Technology Stack

- **Framework**: ASP.NET MVC
- **Language**: C#
- **Database**: SQL Server (Entity Framework Database First)
- **ORM**: Entity Framework 6.x
- **Authentication**: ASP.NET Identity
- **UI**: Razor Views, HTML, CSS, JavaScript
- **Architecture Patterns**: 
  - MVC (Model-View-Controller)
  - Repository Pattern
  - Unit of Work Pattern

## 🏗 Architecture

The application follows a layered architecture:

```
┌─────────────────────────────────────┐
│         Presentation Layer           │
│         (Controllers/Views)         │
└─────────────────────────────────────┘
                  │
┌─────────────────────────────────────┐
│         Business Logic Layer         │
│         (ViewModels/Models)          │
└─────────────────────────────────────┘
                  │
┌─────────────────────────────────────┐
│         Data Access Layer            │
│    (Repository/Unit of Work)        │
└─────────────────────────────────────┘
                  │
┌─────────────────────────────────────┐
│         Database Layer               │
│    (Entity Framework/SQL Server)    │
└─────────────────────────────────────┘
```

### Design Patterns Used

1. **Repository Pattern**: Abstracts data access logic
2. **Unit of Work Pattern**: Manages database transactions
3. **Dependency Injection**: For managing dependencies
4. **MVC Pattern**: Separation of concerns

## 📁 Project Structure

```
ESS-Electronic-Store-System/
│
├── Controllers/
│   ├── HomeController.cs          # Home page, cart operations
│   ├── AccountController.cs        # Authentication & authorization
│   ├── ManageController.cs         # User account management
│   └── AdminController.cs          # Admin operations
│
├── Models/
│   ├── Home/
│   │   ├── HomeIndexViewModel.cs   # Product listing view model
│   │   └── Item.cs                 # Shopping cart item model
│   ├── AccountViewModels.cs        # Account-related view models
│   ├── ManageViewModels.cs         # User management view models
│   ├── CategoryDetail.cs           # Category model
│   ├── Shopping.cs                 # Shipping details model
│   ├── EmployeeViewModel.cs       # Employee view model
│   └── IdentityModels.cs          # Identity framework models
│
├── Views/
│   ├── Home/                       # Home page views
│   ├── Account/                    # Authentication views
│   ├── Manage/                     # Account management views
│   ├── Admin/                      # Admin panel views
│   └── Shared/                     # Shared layouts and partials
│
├── Repository/
│   ├── IRepository.cs              # Repository interface
│   ├── GenericRepository.cs        # Generic repository implementation
│   └── GenericUnitOfWork.cs        # Unit of Work implementation
│
├── DAL/                            # Data Access Layer
│   ├── Model1.Context.cs          # Entity Framework context
│   └── [Entity Models]            # Database entity models
│
└── App_Start/                      # Application configuration
```

## 🗄 Database Schema

The system uses the following main database tables:

- **Tbl_Product**: Stores product information
  - ProductId, ProductName, CategoryId, Price, Quantity
  - ProductImage, Description, IsActive, IsDelete
  - CreatedDate, ModifiedDate, IsFeatured

- **Tbl_Category**: Stores product categories
  - CategoryId, CategoryName, IsActive, IsDelete

- **Tbl_Cart**: Shopping cart items
  - CartId, ProductId, MemberId, CartStatusId

- **Tbl_CartStatus**: Cart status types
  - CartStatusId, CartStatus

- **Tbl_Members**: User/member information
  - MemberId, and related user fields

- **Tbl_MemberRole**: User roles
  - Links members to roles

- **Tbl_Roles**: Available roles
  - Role definitions

- **Tbl_ShippingDetails**: Shipping information
  - ShippingDetailId, MemberId, Address, City, State
  - Country, ZipCode, OrderId, AmountPaid, PaymentType

- **Tbl_SlideImage**: Homepage slide images
  - Image data for homepage carousel

## 🔑 Key Components

### Controllers

#### HomeController
- **Index**: Displays products with search and pagination
- **AddToCart**: Adds products to shopping cart
- **RemoveFromCart**: Removes products from cart
- **DecreaseQty**: Decreases product quantity in cart
- **Checkout**: Displays checkout page
- **CheckoutDetails**: Displays checkout details

#### AccountController
- **Login/Register**: User authentication
- **ForgotPassword/ResetPassword**: Password recovery
- **ExternalLogin**: OAuth integration
- **LogOff**: User logout

#### ManageController
- **Index**: User account dashboard
- **ChangePassword**: Password management
- **AddPhoneNumber**: Phone verification
- **EnableTwoFactorAuthentication**: 2FA setup
- **ManageLogins**: External login management

#### AdminController
- **Dashboard**: Admin control panel
- **Product**: Product listing
- **ProductAdd/ProductEdit**: Product CRUD operations
- **Categories**: Category listing
- **AddCategory/UpdateCategory**: Category CRUD operations

### Repository Pattern

#### GenericRepository<T>
Provides generic CRUD operations:
- `GetAllRecords()`: Get all records
- `GetFirstorDefault(int id)`: Get by ID
- `Add(T entity)`: Insert new record
- `Update(T entity)`: Update existing record
- `Remove(T entity)`: Delete record
- `GetListParameter(Expression<Func<T, bool>>)`: Filtered queries
- `GetRecordsToShow()`: Pagination support

#### GenericUnitOfWork
Manages database context and transactions:
- `GetRepositoryInstance<T>()`: Get repository for entity type
- `SaveChanges()`: Commit changes to database
- Implements IDisposable for resource management

### Models

#### Shopping Cart
- **Item**: Represents a cart item with Product and Quantity
- Session-based storage for cart persistence

#### ViewModels
- **HomeIndexViewModel**: Product listing with pagination
- **CategoryDetail**: Category information
- **ProductDetail**: Product information
- **Shippingdetail**: Shipping and payment details

## 🚀 Installation

### Prerequisites

- Visual Studio 2019 or later
- SQL Server 2012 or later
- .NET Framework 4.x
- IIS Express (included with Visual Studio)

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/FurqanMujahid/ESS-Electronic-Store-System-.git
   cd ESS-Electronic-Store-System-
   ```

2. **Database Setup**
   - Create a SQL Server database
   - Update connection string in `Web.config`:
     ```xml
     <connectionStrings>
       <add name="dbMyOnlineShoppingEntities1" 
            connectionString="Data Source=YOUR_SERVER;Initial Catalog=YOUR_DB;Integrated Security=True" 
            providerName="System.Data.SqlClient" />
     </connectionStrings>
     ```
   - Run database scripts to create tables

3. **Restore NuGet Packages**
   - Open the solution in Visual Studio
   - Right-click solution → Restore NuGet Packages

4. **Build the Solution**
   - Press `Ctrl+Shift+B` or Build → Build Solution

5. **Run the Application**
   - Press `F5` or Debug → Start Debugging

## ⚙️ Configuration

### Connection String
Update the connection string in `Web.config`:

```xml
<connectionStrings>
  <add name="dbMyOnlineShoppingEntities1" 
       connectionString="Data Source=localhost;Initial Catalog=ESS_DB;Integrated Security=True" 
       providerName="System.Data.SqlClient" />
  <add name="DefaultConnection" 
       connectionString="Data Source=localhost;Initial Catalog=ESS_Identity;Integrated Security=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### Application Settings
Configure application settings in `Web.config`:
- Session timeout
- Authentication settings
- Email settings (for password reset)
- SMS settings (for phone verification)

## 📖 Usage

### For Customers

1. **Browse Products**
   - Visit the home page to see all products
   - Use the search bar to find specific products
   - Click on products to view details

2. **Shopping Cart**
   - Click "Add to Cart" on any product
   - View cart by navigating to checkout
   - Adjust quantities or remove items

3. **Checkout**
   - Review cart items
   - Enter shipping details
   - Complete purchase

4. **Account Management**
   - Register/Login to create an account
   - Manage profile settings
   - Change password
   - Enable two-factor authentication

### For Administrators

1. **Access Admin Panel**
   - Login with admin credentials
   - Navigate to Admin Dashboard

2. **Manage Products**
   - Go to Admin → Product
   - Add new products with images
   - Edit existing products
   - Set prices and quantities

3. **Manage Categories**
   - Go to Admin → Categories
   - Create/edit categories
   - Activate/deactivate categories

## 🔌 API Endpoints

### Home Controller
- `GET /Home/Index` - Product listing page
- `GET /Home/Checkout` - Checkout page
- `POST /Home/AddToCart` - Add product to cart
- `POST /Home/RemoveFromCart` - Remove from cart
- `POST /Home/DecreaseQty` - Decrease quantity

### Account Controller
- `GET /Account/Login` - Login page
- `POST /Account/Login` - Authenticate user
- `GET /Account/Register` - Registration page
- `POST /Account/Register` - Create new account
- `POST /Account/LogOff` - Logout user

### Admin Controller
- `GET /Admin/Dashboard` - Admin dashboard
- `GET /Admin/Product` - Product list
- `GET /Admin/ProductAdd` - Add product form
- `POST /Admin/ProductAdd` - Create product
- `GET /Admin/Categories` - Category list
- `GET /Admin/AddCategory` - Add category form

## 🔒 Security Features

- **ASP.NET Identity**: Secure user authentication
- **Password Hashing**: Secure password storage
- **Two-Factor Authentication**: Additional security layer
- **Phone Verification**: SMS-based verification
- **External Login**: OAuth integration support
- **Role-Based Authorization**: Access control
- **Anti-Forgery Tokens**: CSRF protection
- **Session Management**: Secure session handling

## 🎨 Features in Detail

### Product Search
- Full-text search functionality
- Stored procedure-based search (`GetBySearch`)
- Pagination support (4 products per page)
- Case-insensitive search

### Shopping Cart
- Session-based cart storage
- Real-time quantity updates
- Automatic cart persistence
- Product duplication handling

### Image Management
- Product image upload
- Image storage in database (byte array)
- File system backup storage
- Image display in views

### Soft Delete
- Categories and products use `IsDelete` flag
- Data preservation for reporting
- Easy restore functionality

## 🧪 Testing

To test the application:

1. **Create Test Data**
   - Add categories through admin panel
   - Add products with images
   - Create test user accounts

2. **Test Shopping Flow**
   - Browse products
   - Add to cart
   - Update quantities
   - Proceed to checkout

3. **Test Admin Functions**
   - Login as admin
   - Add/edit products
   - Manage categories

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is open source and available under the [MIT License](LICENSE).

## 👨‍💻 Developer

**Furqan Mujahid**

## 📧 Contact

For questions or support, please open an issue in the repository.

---

**Note**: This is an educational project demonstrating ASP.NET MVC, Entity Framework, and Repository Pattern implementation for an e-commerce system.

