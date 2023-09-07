# Project Title

Senior .NET Engineer Take-Home Assignment

## Description

Person API in ASP.NET Core using CQRS pattern

### Dependencies

* Used external nuget packages like MediatR, FluentValidation, ErrorOr.

### Executing program

* The solution can be opened in Visual Studio and run from there or it can be run from console
* Step-by-step bullets
1. Open a console window at the Persons.API.csproj file location
2. Run the following command
```
dotnet run
```
3. The project will run and display the port on which it can be accessed. Please note that the project is set to run on port 7272 (for HTTPS). The port can be changed from launghSettings.json under Properties folder. 
4. Please access the swagger UI for the application 
   [here](https://localhost:7275/swagger/index.html)
5. The swagger UI or Postman can be used to test the API

### Endpoints 

1. ```GET /Person/{id} ```

* Retrieves a person record with Id.
* Input is a valid guid that represents the person id    

2. ```PUT /Person/{id}```
* Updates person record's birth date or birth location
* Input:
    * Id of the person whose birth information requires update
    * A JSON request body with birth date and location 
        ```
        {
            "birthDate": "2020-09-07",
            "birthLocation": "Test Location"
        }
        ```

3. ```GET /Person```
* Retrieves all person records
* Input **NONE**

4. ```POST /Person```
* Creates new person record
*Input:
    A JSON request body with details of the person
    ```
    {
        "givenName": "Test",
        "surname": "User",
        "gender": "Male",
        "birthDate": "1970-09-07",
        "birthLocation": "Salt Lake City",
        "deathDate": "2023-01-01",
        "deathLocation": "Unknown"
    }
    ```


## What else could have been done

* Unit tests for Validators
* Versioning using
    * *Microsoft.AspNetCore.Mvc.Versioning* nuget package
* Improving the Domain logic 
    * Birthdate can be a ValueObject with checks for date validity i.e. Not in future and than MinValue
    * Death date can be a ValueObject with checks for date validity like Not before birthdate and greater than MinValue
* Swagger can be improved to show allowed values/formats for user input
* Instead of using AutoValidation from FluentValidation I would opt for pipeline behavior or validating the rules inside Handle method, better yet move them close to the Domain Object if the rules fit there
## Authors

Soma Yarlagadda  


## Acknowledgments

Libraries used

* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://github.com/FluentValidation/FluentValidation/blob/main/docs/index.rst)
* [ErrorOR](https://github.com/amantinband/error-or)

