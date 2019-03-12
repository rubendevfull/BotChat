# BotChat

Requirements
.NET Core SDK	2.2
Visual studio 2017	15.9.4
SQL Server	LocalDB
Internet	To get CDN client libraries

Summary
This is a web chat app developed on .Net Core Framework 2.2. It uses Microsoft Signal R real time application Library to make messages spread to users in room.
There are two default users created when you run first time the app:
User Name	Pass
user1@gmail.com
1234
user2@gmail.com
1234

When you run the app, the first is login form asking for credentials and giving the option to Sign up a new user.
 

When the user logged in it could be open Room1-4 to chat with other users

 
There he can to put messages typing something in box and hitting enter
 
The user can log off doing click on Log Out Menu
 

Technical Facts
It has a brief domain driven design class to represent messages in system located on BotBaseChatDomain project layer using aggregate pattern and seedwork hierarchy.
It has an infrastructure layer which is in charge of the implementation of Domain repository definition. It uses EF Core 2.2 for saving data, and Identity Core 2.2 for authentication. When first time run the project, using code first migrations, two databases are created, one for identity data (users, claims, profile, etc.) and another for chat message data. It is an implementation to SQL Server any version, however the connection strings are for LocalDB version of SQL Server, as default it loads a test message in room 1 database.
It has an MVC razor .Net core project to display the pages and bring JSON data to the client calls. In his heart, it uses Vue Js framework to render Vuetify client components. It Uses ID native for .Net core to use any service of the application. It also has command pattern used with MediaTR to make the ACID transactions that makes changes in DB. It uses Signal R Hubs to spread the messages coming to subscribers’ clients.
As security, it implements .Net Core identity 2.2 and authorization browser cookie based to grant access to specific parts of the application. It uses claims to get specific profile information user.

Update!!
CSV Consumption it is done. Using HttpClient it calls endpoint to get a CSV string response which it serializes to a specific model for MVC service process. It receives commands with structure "/stock=aapl" so in case that "aapl" part is valid for stock market value, it will return the range value market
Db persistent it is done now. Using an injection .net core trick, I could use services into a Signal R hub in order to complete the persistence functionality in the application
Unit test is done now. 9 unit test results have been created. It focuses on specific Broker functionality using Mocking of objects and specific commands "/stock=aapl" like.
Error handling is based on ResponseBasicVm strategy which allows having a general exception control of the application wrapping the responses over specific ResponseBasicVm object.
Size of last messages now is defined by "stackMessages" value in the configuration file.

To-do
CSV Consumption: I did not enough time to develop an abstract HTTP service implementation which allows having a mock-able code.
RabbitMQ: I have not used before this bus, however its popularity, so I spent the major time solving the fixed Signal R issues, so I think it is a matter of time too.
Profiles: I had the idea to offer to the user change/create data related to its profile. Again, I think it is a matter of time.


BotChat
