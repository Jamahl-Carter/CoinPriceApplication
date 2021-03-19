# Coin Price Application
Website to configure your preferred cryptocurrency coin and retrieve pricing details.

## Design
This application has two main components, which interact through HTTP messages.
- Coin Price UI – set preferred coin, refresh coin rate (and observe difference in percentage).
- Coin Price API – endpoint to set preferred coin (persistent in cache), endpoint to fetch price details for preferred coin.

## Approach
1. Designed solution and identified hosting/development workflows.
2. Implemented back-end API with in-memory cache and deployed to Azure App Service (in part to enable easier UI development).
3. Implemented container-ised MVP UI and deployed to Azure App Service.
4. Build docker-compose file to facilitate easier development (e.g. changes to UI code can be reflected in service upon saving).
5. Cleaned up UI workflows and addressed any issues (e.g. incorrect percentage, UI ticks).
6. Final deploys to Azure.
7. Final end-to-end test.
8.	Wrote documentation,

## API features
- Global exception handling.
- API Contract validation – Validation is configured through attributes on the message classes, current validation coin selection is valid.
- Unit testing.
- Dependency injection.
- Swagger UI.
- Extensibility/maintainability – separation of internal components lends well to extending in future (e.g. if we wanted to replace repository layer to use a managed DB).

## How to use application
- Navigate to UI (refer to links below).
- Use drop down and “Set preference” button to set you preferred coin.
- Use “Refresh” button to fetch up to date details.
- Observe the percentage changed for asking price for same coin. If coin preference changed then it will be set to 0%.

## Azure hosting links (please note these are subject to change)
- UI - https://coin-price-ui.azurewebsites.net/
- Swagger UI: https://coin-price-api.azurewebsites.net/index.html
- Open API spec: https://coin-price-api.azurewebsites.net/swagger/v1/swagger.json

## How to run locally
To perform the following you need a GitHub login and docker configured.
- Clone repo: `git clone https://github.com/Jamahl-Carter/CoinPriceApplication.git`
- Navigate to root dir: `cd ./CoinPriceApplication`
- Start application: `docker-compose up -V`
- Consume application:
    - UI: http://localhost:3000
    - Swagger UI (for API): http://localhost:8081/index.html

## Time taken estimates (for better indication please refer to commit frequency)
- 3 hours to complete API and MVP UI.
- 2 hours to update UI, enable docker compose development.
- 0.5 hours to update documentation.
- ~1 hour overall planning (without coding)

## Suggested changes
- API gateway to proxy requests to API instead of exposing app service (including not exposing development swagger UI, please note this was purposely left in to make it easier to showcase).
- Use managed database (e.g. azure cosmos) with exponential retry policies – not added in this case because this is for a single user.
- Move contract to versioned NuGet package.
- User/session management. 
- Optimise UI for better maintainability and operation in production.
- Fault-tolerance improvements – instead of querying the cointree API directly as part of the transaction, utilise a distributed cache (e.g. Redis) so we can synchronously access as part of request and asynchronously populate.
- Scalability improvements – with use of in-memory cache its not feasible to scale out with this application. To improve this, we should replace use of cache with managed data source and configure autoscaling for the app service.
- User experience – overall the user experience through the UI is lacking, this should be improved upon.
