
# ABC Retailers - Part 2

ABC Retailers is an ASP.NET Core MVC web application designed to manage products, customers, and orders for an online retail store.  
In this second phase of the project, all Azure integrations have been refactored to utilize **Azure Functions**, improving scalability, modularity, and separation of concerns.

---

## Features

### Product Management
- Add, view, edit, and delete products.
- Upload product images through Azure Functions, which handle storage in Azure Blob Storage.
- Separate views for admin (with edit/delete capabilities) and customers (view-only).

### Customer Management
- Register and log in using a custom session-based system (no authentication middleware).
- View customer-specific product listings.
- Restrict admin-only pages based on user email.

### Order Management
- Simple ordering workflow: select product → specify quantity → confirm order.
- "Order Sent" confirmation page after placing orders.

### Azure Integration (Updated)
- All interactions with Azure services are routed through **Azure Functions**.
- Azure Functions handle product image uploads, retrieval, and any other Azure Storage operations.
- Provides scalable, serverless backend processes for file handling and data management.

---

## Technology Stack

| Layer            | Technology                          |
|------------------|-----------------------------------|
| Backend          | ASP.NET Core MVC                  |
| Frontend         | Razor Views (HTML, CSS, Bootstrap)|
| Azure Services   | Azure Functions, Azure Blob Storage|
| Language         | C#                                |
| Development IDE  | Visual Studio 2022 (or later)      |

---

## Getting Started

### Prerequisites
- [.NET SDK 7.0+](https://dotnet.microsoft.com/download)
- Azure Storage Account with Blob Storage enabled
- Azure Functions Core Tools (for local function development)
- Visual Studio 2022 (or later) with Azure development workload installed

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/yourusername/ABC_Retailers_Part2.git
   cd ABC-Retailers
Restore packages

bash
Copy
Edit
dotnet restore
Configure Azure Functions and Storage

Update your appsettings.json with Azure Storage and Azure Functions URLs:

json
Copy
Edit
{
  "AzureStorage": {
    "ConnectionString": "Your_Azure_Storage_Connection_String",
    "ContainerName": "productimages"
  },
  "AzureFunctions": {
    "BaseUrl": "https://your-function-app.azurewebsites.net/api"
  }
}
Run the application

bash
Copy
Edit
dotnet run
Run Azure Functions locally (optional)

If you want to test Azure Functions locally, navigate to the Functions project directory and run:

bash
Copy
Edit
func start
Usage
Login / Registration: Create an account or log in to access ordering features.

Admin Access: Use the admin email (Admin@gmail.com) to manage products and view orders.

Product Uploads: Upload product images via Azure Functions; the files are securely stored in Azure Blob Storage.

Ordering: Browse products, place orders, and get an order confirmation.

Future Enhancements
Integration with Azure Table Storage for detailed product and order metadata.

Email notifications for order confirmation and updates.

Payment gateway integration for secure transactions.

Advanced product search and filtering capabilities.

License
This project is licensed under the MIT License. See the LICENSE file for details.












