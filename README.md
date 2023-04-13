# AttendancePro
AttendancePro is an attendance management web application that allows users to register and log in to the system, and then record their daily attendance. The application provides an easy-to-use interface that enables users to view their attendance history.

The backend of AttendancePro is developed using Asp.net Core Web API, while the frontend is built using Vue.js. The application is deployed on Microsoft Azure and can be accessed through the following link: [https://gray-wave-02a77e600.3.azurestaticapps.net](https://gray-wave-02a77e600.3.azurestaticapps.net/)

## Features
-   User registration and login
-   Recording of daily attendance
-   View attendance history

## Technologies

AttendancePro uses several open-source projects to work properly:

- [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-7.0) - used for building HTTP services
- [VueJS](https://vuejs.org/) - client-side application development
- [Vuetify](https://vuetifyjs.com/en/) - Vue Component Framework

## Getting Started

To get started with AttendancePro, you will need to clone the project from the GitHub repository:

    git clone https://github.com/PialKanti/AttendanceManagement.git

## Prerequisites
-   [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
-   [Node.js](https://nodejs.org/en/)

## Installation
1.  Navigate to the  ***AttendanceManagement/ClientApp*** directory and install the required packages using the following command:
	```
	npm install
	```


3.  Navigate to the  ***AttendanceManagement*** directory and run the following command to start the backend server:
    ```
    dotnet run
    ```
4.  Open a new terminal window and navigate to the ***AttendanceManagement/ClientApp*** directory. Run the following command to start the frontend server:
    ```
    npm run serve
    ```
5.  Open a web browser and navigate to [http://localhost:8080](http://localhost:8080/) to view the application.

## Deployment
The application is deployed on Microsoft Azure and can be accessed through the following link: [https://gray-wave-02a77e600.3.azurestaticapps.net](https://gray-wave-02a77e600.3.azurestaticapps.net/)
