# Day 19 - May 29, 2025
## Session Overview
- Mappers
    - Manual Mapper
    - Built-in AutoMapper
- Possible Object Cycle Detected Error
    - Response DTO method
    - Json Serializer Configuration method
    - NewtonSoftJson library
- Stored procedures
- Transactions

## Mappers
- Convert objects from one type to another, often between different layers of the application
- **Security:** Expose only the necessary fields of the private domain object to the client

### Manual Mapper
- User-defined conversion logic

*Domain Model*
```cs
public class User
{
    public int Id;
    public string Name;
    public string PasswordHash;
}
```
*DTO Model*
```cs
public class UserResponseDTO
{
    public int Id;
    public string Name;
}
```
*Domain Model to DTO Mapper*
```cs
public class UserMapper
{
    public UserResponseDTO MapUserResponseDTO(User user)
    {
        return new UserResponseDTO {
            Id = user.Id,
            Name = user.Name
        };
    }
}
```
### AutoMapper
- library that automaitcally maps properties with the same name and compatible types between objects
- **Installation**
```bash
dotnet add package AutoMapper
```
- **Usage**
```cs
var config = new MapperConfiguration(cfg => {
    cfg.CreateMap<User,UserResponseDTO>();
})

var mapper = config.CreateMapper();
UserResponseDTO response = mapper.Map<UserResponseDTO>(user);
```

## Possible Object Cycle Detected Error
- Occurs when returning EF Core entities with navigation properties directly from an API endpoint
- The serializer will try to convert the object graph to JSON, but gets stuck in a loop due to circular reference.
- *EG:* Doctor has a navigation property of list of appointments. When expanding an appointment, it contains a navigation property to the doctor. This again expands the doctor object, which will again loop to appointment object.
```
{
    "Name":"Varun",
    "Appointments: [
        { "Patient":"Deepak", "Doctor": {
            "Name": "Varun",
            "Appointments": [
                { "Patient":"Deepak", "Doctor": {
                    ....
                }},
                ....
            ]
        }},
        ....
    ]
}
```
