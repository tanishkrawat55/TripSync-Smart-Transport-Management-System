# 🚚 TripSync – Smart Transport Management System

A modern **Transport Management System** built using **ASP.NET Core Razor Pages (.NET 8)** to manage vehicles, drivers, routes, and trip operations efficiently.
Designed for **students, demos, and portfolio projects**, with easy local setup using **SQL Server (SSMS)**.

---


## ✨ Overview

TripSync helps streamline transport operations with role-based access and clean Razor Pages UI.
It enables administrators and users to manage transport workflows from a centralized dashboard.

---


## 🔥 Key Features

* 🔐 Authentication & role-based access
* 🚗 Vehicle management
* 🧑‍✈️ Driver management
* 🗺️ Route planning
* 📅 Trip scheduling & tracking
* 🔍 Search & filtering
* 📊 Simple admin-friendly UI

---


## 🛠 Tech Stack

| Layer    | Technology                        |
| -------- | --------------------------------- |
| Backend  | ASP.NET Core Razor Pages (.NET 8) |
| Language | C#                                |
| ORM      | Entity Framework Core             |
| Database | SQL Server (SSMS)                 |
| Frontend | Razor Pages + Bootstrap           |
| Tools    | Visual Studio, Git, GitHub        |

---


## 📋 Prerequisites

Before running the project, install:

* ✅ .NET 8 SDK
* ✅ Visual Studio 2022/2023
* ✅ SQL Server + SSMS
* ✅ Git (optional but recommended)

---


## ⚙️ Installation & Setup

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/tanishkrawat55/TripSync-Smart-Transport-Management-System.git
cd TripSync-Smart-Transport-Management-System
```

### 2️⃣ Open the Project

* Visual Studio → Open `.sln` file

### 3️⃣ Restore Packages

```bash
dotnet restore
```

---


## 🗄 Database Configuration (IMPORTANT)

This project uses **SQL Server via SSMS**.

You MUST add your own server name inside:

```
appsettings.json
```

### ✏️ Update Connection String

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TMSDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### 💡 Example Server Names

| Setup            | Server Name Example         |
| ---------------- | --------------------------- |
| LocalDB          | (localdb)\MSSQLLocalDB      |
| Local SQL Server | DESKTOP-ABC123\SQLEXPRESS   |
| Custom Server    | YourMachineName\SQLInstance |

---


## 🧬 Database Setup (EF Core)

Run migrations to create database:

```bash
dotnet ef database update
```

If EF tools not installed:

```bash
dotnet tool install --global dotnet-ef
```

---


## ▶️ Run the Project

### Using CLI

```bash
dotnet build
dotnet run --project TransportManagementSystem
```

### Using Visual Studio

* Set project as Startup Project
* Press **F5** or **Ctrl + F5**

App runs on:

```
https://localhost:5001
```

---


## 👤 Default Usage

* Register a new account via Register page
* Login and explore modules
* Admin roles can be assigned manually if needed

---


## 🚀 Deployment Options

* 🌐 Azure App Service
* 🖥 IIS Hosting
* 🐳 Docker (future enhancement)

---


## 📌 Future Improvements

* 📱 Mobile responsive dashboard
* 📊 Analytics & reporting
* 🔔 Real-time notifications
* 🌍 API integration

---


## 🤝 Contributing

Pull requests are welcome!
For major changes, open an issue first to discuss what you'd like to improve.

---


## 📜 License

This project is for **educational and portfolio purposes**.
You may modify and reuse with credit.

---


## 👨‍💻 Author

**Tanishk Rawat**
🔗 GitHub: [https://github.com/tanishkrawat55](https://github.com/tanishkrawat55)

---


## ⭐ Support

If you like this project:

* ⭐ Star the repo
* 🍴 Fork it
* 🧠 Use it for learning

---

> Built with ❤️ using .NET 8 and lots of debugging ☕
