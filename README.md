# Promotion Rule Engine
Technology & Frameworks
.Net Core 3.1 with Swashbuckle Open API is been used for development and documentation of the API. 
The solution isn't a full fledged complete one, skeleton of the solution is kept with various layers.

## Strategy

Idea is to expose an API where multiple cart items will be sent and promotional discounts are calculated on them and return the final computed value. 
In the interest of time, TDD wasn't followed. Libraries such as business, core and repositories yet to have those unit tests.

## Design

Promotional service which exposes an API, allows the users to send requests.
Upon receiving the request, validation has to be done(yet to implement, either via fluent validation availble in .net core or custom ones).
Model bind happens and routed to the controller action method accordingly, which routes to the handler in Business layer to calculate the desired output.
Business layer works with various libraries repo, core(which hold our entity classes) to process cart value and sends back the response.
