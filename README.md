# Xero Products



Release Notes: 
--------------
20240830 - Renamed/Rebranded to XeroProducts (from "refactorthis"). Better fitting descriptive title for the solution/project.
20240830 - Upversioned Api project from .Net Framework 4.5 to .Net 8.0 (Improved support for future devwork, security, compatibility, etc)
20240830 - Now using Swagger/OpenAPI (for better documentation)
20240830 - Now supports Https (increased security)
20240831 - Models split up into "Types" and "Providers" (modularise/split schema/data-shape from logic)
20240831 - New Business Logic (BL) Layer to hold logic away from API (for modular/re-use)
20240831 - New Data Access Layer (DAL) to remove data store interaction from BL (separation of concerns)
20240831 - Implemented IoC across BL and DAL to support future extension and testing
20240831 - Altered all methods to run asynchronously (for faster performance/future multi-threading improvements)
20240902 - Added covering Unit Tests for BL