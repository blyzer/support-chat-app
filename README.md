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

Right click on the solution and open properties. Enable 'multiple startup projects' and change the drop down value to 'start' for two API projects.<br />
![image](https://user-images.githubusercontent.com/38711024/137648960-c04eb56c-c41e-47a4-92d4-7d8c0e64829f.png)


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
This has all the chat related endpoints. This also has role base authorization.<br />
![Chat API](https://user-images.githubusercontent.com/38711024/137648843-2c0978a8-e867-4c22-a948-074f3b403fe4.png)

### Database
Once you run the project database will be created with default data.<br />
![data base](https://user-images.githubusercontent.com/38711024/137648900-bc65001c-da50-4e42-9df3-fcb3bd1b94f1.png)<br />
![master data](https://user-images.githubusercontent.com/38711024/137648914-d27d3c39-fa64-49bc-9138-502a96325719.png)

## Functionality

Once you start the project two Swagger UIs will open.<br>
Authentication API swagger<br>
![Authorization API](https://user-images.githubusercontent.com/38711024/137649027-24cfbf24-4dba-4e85-97cd-cc1a6f44fba9.png)<br />

Chat API Swagger<br>
![chat api swagger](https://user-images.githubusercontent.com/38711024/137649058-07cd6b34-318c-4bbe-9987-40b538d7f689.png)<br />

First you should log in to the system by login endpoint. <br />
![loginRequest](https://user-images.githubusercontent.com/38711024/137649105-d21af854-fec6-4b71-8713-6f4b575c3c52.png)

After you are successfully loged in the system, you can copy the token from the response.<br />
![login response](https://user-images.githubusercontent.com/38711024/137649156-cca8580a-67de-4542-a95c-a3ee9e0d5d02.png)

Response for correct token<br />
![Results with correct token](https://user-images.githubusercontent.com/38711024/137649183-3d43f49d-84ea-4783-b315-6fd832cd1ef2.png)

Response for the incorrect token<br />
![results with incorrect token](https://user-images.githubusercontent.com/38711024/137649197-799042b0-2b27-4f13-9a29-7fa4892a1d7e.png)

Now trigger the create chat endpoint with the correct token. Only clients have the access to create chat endpoint.
Now check the RabbitMQ queue tab.<br />
![RabbitMq](https://user-images.githubusercontent.com/38711024/137649301-20d38a5e-80cf-4cf6-b20e-b042f2fb7fc6.png)

You can see application publish data to queue and consume data from the queue to create chat sessions.<br />

### Agent Selection Logic

First need to choose which team is available. Team A is available from 6 AM to 2 PM, Team B is available from 2 PM to 10 PM and Team C is avaialble from 10 PM to 6 AM. Overflow team is not available with the Team C.<br />
![Select team](https://user-images.githubusercontent.com/38711024/137649461-3ae9846b-d692-43e2-8912-9a35a5b7bbe2.png)

Take the agents from the user table. Agent list should be in the ascending order with the chat count. That means the agents who has assigned minimun chat count will be in the top.<br />
![agent query](https://user-images.githubusercontent.com/38711024/137649526-90344170-8fd2-42bd-b9bd-2b8dc360c2d8.png)

System chooses the agents from junior to senior priorities.<br />
![select agent](https://user-images.githubusercontent.com/38711024/137649587-5da34c21-4294-4f21-aa16-656438facb58.png)

Once the agent is selected chat session will be created with the agent and client and add more record to the AgentChat table to track the chat count.
![create chat and agent chat recors](https://user-images.githubusercontent.com/38711024/137649633-3a7c1c1c-e154-46c5-bd4a-f390e181c52b.png)

This is the initial implementation of the application. Actual chat functions will be available with the UI later.

## User Credentials

- Username: john@supportchat.com
- Password: john
- Role: Client

- Username: admin@supportchat.com
- Password: admin
- Role: Admin
[Admin functions are not implemented yet.]

- Username: teamA1@supportchat.com
- Password: teamA1
- Role: Agent









