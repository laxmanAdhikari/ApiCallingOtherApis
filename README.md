# ApiCallingOtherApis
This repo includes frontend api that consumes othe backend api (downstream apis)

The Frontend API which is secured via OAuth using OKTA. 

## Getting Started

To set up the solution, floow the below steps.
Have installed and running:
 - Visual Studio 2022, with .Net 7
 - Setup OKTA account (used the developer version registration)

## Get/Build code

```
git clone https://github.com/laxmanAdhikari/ApiCallingOtherApis.git

```
Open the solution[ApiCallingOtherApis.sln] using visual studio 2022 in the root of the folder ApiCallingOtherApis. 
Right click Solution => Properties => Select Multiple startup ptojects
OrderProcessingApi (FrontEndApi)
PrderProcessing.CustomerAPi (BackEndApi One)
OrderProcessing.ProductApi (BackendApi two)

## Environment Variables
Use .env file to run the solution. 
```
Please replace the following environment values. In order to bypass the security use BYPASS_SECURIT to 1 otherwise assign 0 and provide other values.

# Environment variables
CLIENT-ID=client-id
CLIENT-SECRECT=client-secrect
AUDIENCE=audience
GRANT-TYPE=grant-type
TOKEN-URL=token-url
BYPASS_SECURITY=1
```

``` =========  Running application ====================
On running via visual studio, the three browser window will be opened with swagger UI with all the API endpoints

1 FrontEndPAi (http://localhost:8080/swagger/index.html)
![image](https://github.com/laxmanAdhikari/ApiCallingOtherApis/assets/6294560/194c143d-1965-41a7-a04b-3da22b8322e7)

Click /Order/api/v1/placeorder and exdecute it.

2 BackendApiOne (http://localhost:8081/swagger/index.html)

![image](https://github.com/laxmanAdhikari/ApiCallingOtherApis/assets/6294560/cebbbd64-e715-4b65-9fa5-9ea4c728f2f7)

3 BackendApiTwo (http://localhost:8082/swagger/index.html)
![image](https://github.com/laxmanAdhikari/ApiCallingOtherApis/assets/6294560/693a1292-bd26-4eb3-b954-5e71d5b20260)

```` ======================================================

``` ===== Running Unit tests ========================
Open tests from Test => Test Explorer
The following tests will be visible.

<img width="605" alt="image" src="https://github.com/laxmanAdhikari/ApiCallingOtherApis/assets/6294560/de292906-f66e-4cb5-8bcb-88d6939b7709">

```` ===============================================
