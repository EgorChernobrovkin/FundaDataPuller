# FundaDataPuller

# Greetings

Hello!   
This is the home assignment for Funda.nl implemented by Egor Chernobrovkin.   
This application can pull information about real estate from Funda.nl and create Top 10 real estate agents.   

# About
The application consist of two microservices Funda.DataPuller.Api, Funda.DataPulling.Service and Angular SPA.   

**Funda.DataPulling.Service** - microservice (Service Worker) for pulling information from Funda.nl endpoint. In order to keep request limit for Funda.nl enpoint (100 per minute) this microservice does one job at time. All requests that come during the job wait for their queue.   

**Funda.DataPuller.Api** - microservice (Web Api) for communicating with UI. It sends messages via a service bus to Funda.DataPulling.Service in order to pull data from Funda.nl enpoint. Also, it creates connection (SignalR) with clients for two-way communication.    


# How to run   

## Back-end  

Funda.DataPuller.Api and Funda.DataPulling.Service uses Rebus for messaging and file storage as a transport. So, in order to work correctly for the first services must be started in the following order:
1. Funda.DataPulling.Service
2. Funda.DataPuller.Api

## Front-end

Client application is implemented using Angular 8+, so in order to run it you need to install NPM and Angular CLI.
Use the following commands in the `*\FundaDataPuller\src\ClientApp` directory:    
`npm start`

# Have fun!
