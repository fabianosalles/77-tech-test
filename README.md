# 77-tech-test

This project implements the requirements described in the docs.

The backend was designed with scale in mind. 
I used the mediator pattern to isolate the layers and facilitate CQRS implementation. 
Now we have only one db for reading and writing but, we could easily generate data mutation events on the infrastructure layer and publish it to a topic on `RabbitMQ` or `Azure Service Bus` to be consumed on the slave/reading db.

I tried to build everything from scratch using the minimum number of external libraries. 
The Vue application can greatly benefit from adopting a data validation library and extracting the API communication logic in a single, crosscutting service class, but I'm afraid my time is out. A structured logging library like Serilog, can also be a great addition on the backend.

I hope you enjoy reviewing this, and please, feel free to get in touch in case of doubts.

Thanks!


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
A list of screenshots highlighting key points of the project.

Simple Book List Browsing
![image](https://github.com/user-attachments/assets/0108aa55-ad21-4cfa-917a-491df6235ab5)

Simple modal form for adding new books <br>
![image](https://github.com/user-attachments/assets/ec7b44ed-6e51-4bc3-98c2-9aad06d60b53)

Small screen media query breakpoint<br>
![image](https://github.com/user-attachments/assets/9d38329d-f1fa-4b7d-94ba-0a2c72ee3926)


Integration and Unit Tests passing<br>
![image](https://github.com/user-attachments/assets/3edc3be3-6c84-4826-aae3-94b1158b8400)

A Simplified `Clean Architecture` organization combined with `CQRS` pattern <br>
![image](https://github.com/user-attachments/assets/85489282-eebb-4a1e-8dd2-72abb4da9f64)
