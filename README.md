Instructions

*** Requirements ***
Microsoft Visual Studio (.NET 7 compatible version) installed

*** How to run ***
1. Open the TodoAppWebApi.sln in Visual Studio.
2. Build the solution. (if NuGet restore is needed, please run "Restore NuGet packages")
3. Set TodoAppWebApi as default project, and run the application. (You can use https or IIS as host, whatever you prefer.)
4. You should see the Swagger documentation opens in the browser.

*** How to test the API ***
- You can test from the Swagger documentation (https://localhost:5001/swagger/index.html)
- You can test it from any web API testing platform (e.g. Postman), the api endpoint url is https://localhost:5001/api/TodoItemModels
