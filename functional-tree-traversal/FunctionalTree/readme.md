# Database configuration

The database server and name need to be configured in

- `Web.config` in project WebApi (currently `FunctionalTree` on `localhost`)
- `App.config` in project IntegrationTests (currently `FunctionalTreeTest` on `localhost`)

Simply change the `SqlDbContext` connection string and set the **Data Source** and **Initial Catalogue**.

Running the WebApi project or the Integration Tests will automatically create the database if it doesn't exist.

# Manually testing the API
To manually interact with the API, install Postman REST client on chrome.