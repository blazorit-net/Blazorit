# Blazorit
## The Blazor Framework for Web Artisans (fullstack or command)

##### At the beginning, you need add file "appsettings.json" in this template solution in folder /Blazorit/Server/appsettings.json.
You have to add this code to the file /Blazorit/Server/appsettings.json:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "server=server_address;Port=5432; Database=db_name; Username=user_name; Password=pass"
  },
  "AppSettings": {
    "SecurityKey": "some long secret key"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
And copy this file to folder /Blazorit/tests/Blazorit.ConsoleApp/

For production: you have to change DefaultConnection string for your Database. Also, you can change SecurityKey in this file.

Then, you need run command for migrations to your db: 
```
dotnet ef database update
```
