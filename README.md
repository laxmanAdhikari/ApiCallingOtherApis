# ApiCallingOtherApis
This repo includes the samples to invoke the downstream APIs

The Frontend API which is secured via OAuth using OKTA. Please replace the following environment values. In order to bypass the security use BYPASS_SECURIT to 1 otherwise assign 0 and provide other values.

# Environment variables
CLIENT-ID=client-id
CLIENT-SECRECT=client-secrect
AUDIENCE=audience
GRANT-TYPE=grant-type
TOKEN-URL=token-url
BYPASS_SECURITY=1

You can test the application by donloading the repo and make the multiple projects start up 
OrderProcessingApi (FrontEndApi
PrderProcessing.CustomerAPi (BackEndApi One)
OrderProcessing.ProductApi (BackendApi two)

