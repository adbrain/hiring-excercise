# Database configuration

The database server and name need to be configured in

- `Web.config` in project WebApi (currently FunctionalTree on localhost)
- `App.config` in project IntegrationTests (currently FunctionalTreeTest on localhost)

Simply change `SqlDbContext` connection string and set the **Data Source** and **Initial Catalogue**.

# Manually testing the API
To manually interact with the API, install Postman REST client on chrome.