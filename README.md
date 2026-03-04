# TripXTest 

**To test this application please use the collections provided, we have created collections both for Postman and Bruno, also there is a standard openapi json provided**

**Also since this is a public repo, add the test urls from the assignment in appsettings.json**

## Search for arrangement
- Retrieve data from external API providers  
- Enrich the arrangements with Offers 
- Group the arrangements in a single Option
- Save the arrangements in-memory cache


## Book an arrangement 
- Book the arrangement using the chosen option code 
- Fire a background event for completing the booking in previously randomly generated seconds

## Check the status of the booking

- It returns enum value: [Pending, Complete, Failed]

