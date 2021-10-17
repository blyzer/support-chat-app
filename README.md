# support-chat-application.

## Install RabitMQ

Need to install the RabbitMq for windows/linux.
For windows: https://www.rabbitmq.com/install-windows.html

After installing the RabbitMq goto the 'C:\Program Files\RabbitMQ Server\rabbitmq_server-3.9.7\sbin' through the file manager and open the cmd. run below command.
rabbitmq-plugins.bat enable rabbitmq_management

Then go to the http://localhost:15672/ and add 'guest' as both username and password.

Then go to the queue tab to check the overview.<br />
![image](https://user-images.githubusercontent.com/38711024/137648170-87e9e9dd-1f58-4e54-96d3-ec1a08a3ab8b.png)

## Setup the project

Clone the project through the github and open it using the Visual Studio.

Go to the AuthorizationAPI and appsettings.development.json file to add the connection string.<br />
![Auth API configurations](https://user-images.githubusercontent.com/38711024/137648268-7b15f8ba-d888-4544-bea9-5f090e780eb2.png)

Go to the ChatAPI and appsettings.development.json file to add the connection string. Please check the RabbitMQ configurations as well.<br />
![Chat API configurations](https://user-images.githubusercontent.com/38711024/137648341-ca4e3776-daa1-4426-8428-bc738a5380f9.png)

## Project Architecture

### Technology Stack

- .NET Core
- SQL Sever
- EF Core - Code First
- RabbitMQ - Message-Broker

The solution has 5 projects.<br />
![Project Files](https://user-images.githubusercontent.com/38711024/137648448-3f171d87-0e4e-46e5-a073-c681aaffe771.png)

### Support-Chat-App.AuthenticationAPI
This is for the authentication and autorization purpose. The application has main 3 user roles: Admin, Client and Agent.<br />
![Authentication API](https://user-images.githubusercontent.com/38711024/137648525-75fa78c7-2be8-4bba-9677-249d35d3d064.png)

### Support-Chat-App.Data
This class library has all the common data related classes such as data transfer objects, enums, entities, helper classes, request models and response models. Application has been develop using ef core code first approach.<br />
![Data class lib](https://user-images.githubusercontent.com/38711024/137648737-a815ff77-5a5d-478c-9802-523a9ebfc629.png)

### Support-Chat-App.Managers
This class library contains all the business logics and RabbitMQ functions.<br />
![manager class lib](https://user-images.githubusercontent.com/38711024/137648772-2bdccf63-5144-4dc0-9920-a7c9838ef8a0.png)

### Support-Chat-App.Repositories
This class library has all the LINQ queries and authorization related functions.<br />
![repository class lib](https://user-images.githubusercontent.com/38711024/137648809-57c04abf-72e8-4b61-93ba-3fc818ab6466.png)

### Support-Chat-App-ChatAPI
This has all the chat related endpoints. This also has role base authorization.
![Chat API](https://user-images.githubusercontent.com/38711024/137648843-2c0978a8-e867-4c22-a948-074f3b403fe4.png)







