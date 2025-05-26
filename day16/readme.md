# Day 16 - May 26, 2025
## Session Overview
- DLL vs EXE
- HTTP Methods
- HTTP Status Codes
- Dotnet CLI Commands
- Installing Postman
- Controllers

## DLL vs EXE
- **DLL:** Dynamic Link Library. A resuable library of code meant to be used by other programs. Cannot run on it's own (doesn't have an entry point)
- **EXE:** Executable File. A standalone application that can be run directly. Contains an entry point of execution

## HTTP Methods
[Reference](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Methods)
- set of request methods to indicate purpose of request
- **GET:** requests a representation of the specified resource. Request should only retrieve data and should not contain a request content
- **POST:** submits an entity to the specified resource, often causing a change in state or modification on the server
- **PUT:** replaces all current representation of the target resource with the request content
- **DELETE:** delete a specific resource
- **PATCH:** applies partial modification to a resource

## HTTP Status Codes
[Reference](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status)
- codes to indicate whether a HTTP request has been successfully completed

| Reponse Group          | Status Code Range |
| :--------------------: | :---------------: |
| Informational Responses| 100 - 199         |
| Successful Responses   | 200 - 299         |
| Redirection Messages   | 300 - 399         |
| Client error Responses | 400 - 499         |
| Server error Responses | 500 - 599         |

- **200 OK:** The request succeeded
- **201 Created:** The request succeeded and a new resource was created. Typically a response for POST and PUT requests
- **400 Bad Request:** The server cannot or will not process the request due to a client error
- **401 Unauthorized:** The client must authenticate itself to get the requested resource
- **403 Forbidden:** The client has autheticated itself but doesnot have access rights to the content
- **404 Not Found:** cannot find the requested resource
- **405 Method Not Allowed:** request method is known by the server but is not supported by the target resource
- **500 Internal Server Error:** server had encounter an error

## Dotnet CLI Commands
[Reference](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet)
- To create a new project [Reference](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new)
```
dotnet new <template_name> -n <project_name> <options/flags>
```
- To build the project
```
dotnet build
```
- To run the project
```
dotnet run
```

## Installing Postman
[Link](https://www.postman.com/downloads/)

## Controllers
[Reference](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/aspnet-mvc-controllers-overview-cs)
- responsible for responding to requests made to an ASP.NET Web API
```cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class DoctorController : ControllerBase
{
    static List<Doctor> doctors = new List<Doctor>
    {
        new Doctor{Id=101,Name="Ramu"},
        new Doctor{Id=102,Name="Somu"},
    };
    [HttpGet]
    public ActionResult<IEnumerable<Doctor>> GetDoctors()
    {
        return Ok(doctors);
    }
    [HttpPut]
    public ActionResult<Doctor> UpdateDoctor([FromBody] Doctor doctor)
    {
        var existingDoc = doctors.FirstOrDefault(doc => doc.Id == doctor.Id);
        if (existingDoc == null)
        {
            return NotFound();
        }

        existingDoc.Name = doctor.Name;
        return Ok(doctor);
    }
}
```
- **ApiController Attribute:** applied to controllers to enable API-specific behaviours for improving the developer experience of building Web APIs. [Reference](https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-9.0#apicontroller-attribute)
- **Route Attribute:** Specifies the URL pattern that should be used to inke a particular action. The `[controller]` is a placeholder that will be automatically replaced by the name of the controller without the "Controller" suffix
- **ControllerBase class:** All Web API controllers must inherit the `ControllerBase` class. It provides many properties and methods that are useful for handling HTTP requests
- **Controller Actions:** An action/method that is invoked when a specific route is called. Must be a public member of the controller class. The method used by a controller cannot be overloaded. The method used by a controller cannot be static.
- **Action Results:** The response that controller action returns to the request. There are different types of action results available based on the type of response returned. The base class for all the action results is `ActionResult`
- **Binding Source Attribute:** defines where an action's parameter is found in the request. [Reference](https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-9.0#binding-source-parameter-inference)
- ***Note: Controllers are stateless and instantiated per request.*** For each HTTP request, a new instance of the controller class is created to handle that request. Hence, any variable declared in the controller exist only for the lifetime of that single request. So, any changes made to the variables inside the controller in one request will not persist to another request. To persist data, there are several techniques available such as declaring the variable as `static` or using database to store the data.