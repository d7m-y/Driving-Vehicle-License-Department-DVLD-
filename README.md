# Driving & Vehicle License Department (DVLD) System

## About The Project
A comprehensive enterprise-level management system for handling driving licenses, vehicle registrations, and departmental operations. Built to simulate real-world governmental applications. 

**30-Second Technical Summary:**
* **Architecture:** 3-Tier (Presentation, Business Logic, Data Access).
* **Data Strategy:** ADO.NET for high-performance database interactions.
* **Key Modules:** People Management, User Authentication, Application Processing, License Issuance, Test Scheduling.
* **Design Patterns:** Layered Architecture, Singleton, Encapsulation.

## Features
* **People & User Management:** Centralized registry and Role-Based Access Control (RBAC).
* **Application Life Cycle:** Automated handling of New, Renewed, and Replacement licenses.
* **Test Management:** Multi-stage testing workflows (Vision, Theory, Practical).
* **License Operations:** Dynamic class definitions, fee constraints, and license detention processing.

## Tech Stack
* **Language:** C# (.NET Framework)
* **UI:** Windows Forms (WinForms)
* **Database:** Microsoft SQL Server
* **Data Access:** ADO.NET (Stored Procedures & Parameters)

## Folder Structure
```text
DVLD-System/
├── DVLD_Solution.sln
├── DataAccessLayer/
├── BusinessLayer/
├── PresentationLayer/
└── Database/

Installation
Execute the database script in SQL Server Management Studio.

Update the database connection string in DataAccessLayer/ConnectionString.cs.

Open DVLD_Solution.sln in Visual Studio.

Restore NuGet packages if required.

Set PresentationLayer as the startup project and press F5 to run.
