# Migration documentation

Source Github Link: https://github.com/chienvh/ASP.Net-MVC-Entity-Framework-by-Example-Project

## Trying to run the project

Try to run the project by building the source `dotnet run` and directly using the existing build file `dotnet bin/ChienVHShopOnline.dll` both failed.
**Reason:** The application is a .NET Framework application which is Windows only.

## Steps taken to convert

- Create a new .NET Core MVC Web app
- Copying each file and modifying to fix any issues as neccessary
- Installing any dependency requirement that arises along the way

## New MVC Web app

```
dotnet new mvc -n CheinVHShopOnline
```

## Nuget Packages

- Microsoft.EntityFrameworkCore
- Newtonsoft.Json
- PayPal
- Npgsql.EntityFrameworkCore.PostgreSQL
- Microsoft.EntityFrameworkCore.Design
- Swashbuckle.AspNetCore
- X.PagedList - PagedList is depreceated (https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application#install-the-pagedlistmvc-nuget-package)
- ClosedXML
- QuestPdf
