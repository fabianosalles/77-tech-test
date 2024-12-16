# 77-tech-test

## Database Migrations

To init the database, run the following command and the `LMS.BookInventory.Infra` directory

```shell
 dotnet ef migrations add Initial --s ../LMS.BookInventory.Api/LMS.BookInventory.Api.csproj --context LMS.BookInventory.Infra.Database.BookContext 
 ```

 To update the database...

 ```shell
 dotnet ef database update -s ../LMS.BookInventory.Api/
 ```