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


## Screenshots
Simple Book List Browsing
![image](https://github.com/user-attachments/assets/0108aa55-ad21-4cfa-917a-491df6235ab5)

Simple modal form for adding new books
![image](https://github.com/user-attachments/assets/ec7b44ed-6e51-4bc3-98c2-9aad06d60b53)


Small screen media query breakpoint

![image](https://github.com/user-attachments/assets/9d38329d-f1fa-4b7d-94ba-0a2c72ee3926)


