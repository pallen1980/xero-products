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


## Requirements:
----------------
You'll need to install the following to build/run the solution...  
1. .Net 8 SDK
2. SQLClient

## Setup Guide:
---------------
1. Clone & Pull the Repo  
2. Open a cmd prompt to the root folder  
3. Run... Build Solution.bat  

By default, the Debug version of this project will build and run using the configuration set up within the XeroProducts.Api\appsettings.Development.json file, but for Release deployment, you'll need to setup the following to secure the solution...

4. Generate your own unique key (to secure Jwt Authentication), Run... Generate Key.bat
5. Add the following environment variables (can be either User or System variables. Both will work)...  
  - XeroProducts__ConnectionString__Default = {DB connection string}  
  - XeroProducts__Auth__JwtConfig__Key = {YOUR_KEY_FROM_THE_PREVIOUS_STEP}  
  - XeroProducts__Auth__JwtConfig__ValidAudience = {YOUR_VALID_AUDIENCE_HOSTNAMERANGE} //eg. localhost:5000 or the domain(s)/host(s) you wish to allow  
  - XeroProducts__Auth__JwtConfig__ValidIssuer =  {YOU_VALID_ISSUER_HOSTNAME} //eg. localhost:5000 or the domain(s)/host(s) you wish to allow  
  - XeroProducts__DAL__Type = "SQL" //[Optional] Can leave out/blank if you wish to use Entityframework DAL, or set value to SQL for direct SQL commands when accessing DB  

NOTE: When adding environment variables, you will need to restart Visual Studio and/or command prompt/powershell you were using to initiate this project (as these values are cached on startup).  


## Release Notes: 
-----------------
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