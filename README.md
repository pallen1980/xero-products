# Xero Products
---------------

Xero Take-home project:- Refactoring a small Web/REST API.  

This improved version of the Web API retains the existing functionality to allow users to perform CRUD operations against a product data source, as well as the following...  

- Up-versioned/Re-wrote for modern .NET v8.0 to provide better future support/longevity (inc. all the standard points that go with newer dotnet versions. eg. performance, compatibility, security, support, extensibility, etc).  
- Added OpenAPI/Swagger support for easier integration testing and documentation of API endpoints  
- Model Validation against all endpoints to provide greater feedback to users of the API
- Central Validation/Exception handling to allow faster extensible development
- Support for HTTPS for increased security  
- Implemented MVVM & Repository Patterns for cleaner code design (Web API Front-End + BL mid-logic layer + DAL for data access)  
- DI/IoC and Inheritance among providers for greater extensibility, modular/re-use (SOLID principles) & support for unit testing 
- Lazy-loaded DI for improved initialisation performance and memory optimisation  
- Covering Unit Tests across the BL to support CI/CD and QA processes/policies  
- CI: Added GitHub Deployment action to build & test when any branch/PR are pushed into main  
- Identity support via basic JWT tokens, to support distributed authentication  
- Native logon support using SHA512 encrypted/hashed passwords  
- User Management (CRUD for users)   
- Authorization: Some endpoints are now locked down unless signed-in/authed  
- ORM support with EntityFramework: Greater DB abstraction, faster dev-time, and out-of-the-box support for more DB platforms  
- Easy to understand fully-commented clean code  
- Added SQL script for DB Creation & Data Population to support easier setup with external DBs  

## Requirements:
----------------
You'll need to install the following to build/run the solution...  
1. .Net 8 SDK
2. SQLClient Driver compatible with SQL Server 2019 (v904) //The DB file has been updated and modified in SQL Server 2019, which means any future access must be by a SQL Client capable of opening DB files created in 2019/v904)

## Setup Guide:
---------------
1. Clone & Pull the Repo  
2. Open a cmd prompt to the root folder  
3. Run... Build Solution.bat  

By default, the Debug version of this project will build and run using the configuration set up within the XeroProducts.Api\appsettings.Development.json file, but for Release deployment, you'll need to setup the following to secure the solution...

4. Generate your own unique key (to secure Jwt Authentication), Run... Generate Key.bat
5. Add the following environment variables (can be either User or System variables. Both will work)...  
  - XeroProducts__ConnectionStrings__Default = {DB connection string}  
  - XeroProducts__Auth__JwtConfig__Key = {YOUR_KEY_FROM_THE_PREVIOUS_STEP}  
  - XeroProducts__Auth__JwtConfig__ValidAudience = {YOUR_VALID_AUDIENCE_HOSTNAMERANGE} //eg. localhost:5000 or the domain(s)/host(s) you wish to allow  
  - XeroProducts__Auth__JwtConfig__ValidIssuer =  {YOU_VALID_ISSUER_HOSTNAME} //eg. localhost:5000 or the domain(s)/host(s) you wish to allow  
  - XeroProducts__DAL__Type = "SQL" //[Optional] Can leave out/blank if you wish to use Entityframework DAL, or set value to SQL for direct SQL commands when accessing DB  

NOTE: When adding environment variables, you will need to restart Visual Studio and/or command prompt/powershell you were using to initiate this project (as these values are cached on startup).  

## Optional Setup - SQL Database
--------------------------------

Rather than deploying the individual Database file (database.mdf) along with the solution, I have enclosed a SQL script that will allow you to setup your own DB on whatever DB provider you'd like.

You'll need to run it as the "sa" account or another account with full admin rights.

It will create the database, a login & user account, all tables and seed them with default data.

Open the following script and run it on your SQL compatible database...

SQL/XeroProduct - Create & Populate DB.sql

You can then access the database with the following login account...

user: XeroUser
Pass: J3tP%ck!

Note: You'll also find a script... TSQL Create Hashed Password.sql which will allow you to create your own hashed password, which you can replace the one in the script before running it.

## User Guide
-------------

### Unauthenticated/Open Endpoints
----------------------------------

The following endpoints are open and can be hit without any authentication...

POST /api/auth/login  
GET /api/products  
GET /api/products/{name}  
GET /api/product/{id}  
GET /api/product/{productId}/options  
GET /api/product/{productId}/option/{id}  
GET /api/user/{id}  

### Authenticated Endpoints
---------------------------

The other endpoints, require authentication via a jwt token.

By default, the system has an admin account which you can auth with, and create more users.

You can login to the admin account with the following details...

POST /api/auth/login  
username: system.admin  
password: Password1  

The resulting token will then allow you to hit the other endpoints using this as the "Bearer: {token}" in the head of each request.  

The remaining endpoints that require authentication are...  

For product CRUD...  
POST /api/product/{id}  
PUT /api/product/{id}  
DELETE /api/product/{id}  

For product option CRUD...  
POST /api/product/{productId}/option  
PUT /api/product/{productId}/option/{id}  
DELETE /api/product{productId}/options/{id}  

For user CRUD...  
POST /api/user  
PUT /api/user/{id}  
DELETE /api/user/{id}  

NOTE: running the solution in Debug/Development mode will enable Swagger and allow you to see and test all the endpoint from a web front-end.  

### Test Web Server
-------------------

I have set up a test server that will be accessible for a limited time. You can access the solution, which is set up in Debug/Development mode, with Swagger enabled, from the following url...  

http://xero-products.duckdns.org (no ssl certificate is applied, so you'll need to ensure you access it at http://)  


## Release Notes
----------------
20240830 - Renamed/Rebranded to XeroProducts (from "refactorthis"). Better fitting descriptive title for the solution/project  
20240830 - Upversioned Api project from .Net Framework 4.5 to .Net 8.0 (Improved support for future devwork, security, compatibility, etc)  
20240830 - Now using Swagger/OpenAPI (for better documentation)  
20240830 - Now supports Https (increased security)  
20240831 - Models split up into "Types" and "Providers" (modularise/split schema/data-shape from logic)  
20240831 - New Business Logic (BL) Layer to hold logic away from API (for modular/re-use)  
20240831 - New Data Access Layer (DAL) to remove data store interaction from BL (separation of concerns)  
20240831 - Implemented IoC across BL and DAL to support future extension and testing  
20240831 - Altered all methods to run asynchronously (for faster performance/future multi-threading improvements)  
20240902 - Added covering Unit Tests for BL  
20240904 - Further MVC model separation (removed types from use in web api endpoint params/endpoints now use their own models - for reduced data transfer/performance/separation-of-concerns)  
20240904 - Added validation for POSTed models (and ability to return more accurate status codes when validation fails)
20240904 - Added support for Http Status Codes when returning from endpoints  
20240904 - Added centralised exception handling  
20240904 - Added Jwt Token-based Auth  
20240906 - Added User Management & Logon Capability  
20240906 - Added BL Dtos to replace Types in Web Endpoints  
20240906 - Added EntityFramework support to DAL providers  
20240906 - Secured DAL connection strings to app-config/environment-variables  
20240907 - Lazy-loading injected providers across all layers + accompanying refactoring  
20240907 - Removed unnecessary/out-of-date packages (reduced dependencies/enforced security)  
20240907 - Ensured the admin account cannot be removed or updated  
