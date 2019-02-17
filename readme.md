# CarAdverts

CarAdverts is a Web API for quering, creating, updating and deleting car adverts.
Build using ASP.NET Core, Entity Framework, DDD aproach and others.

## Usage

Restore nuget packages and run CarAdverts project.
With usage of swagger you can try it in action, just go to /swagger.

## Known issues

- Sorting by Fuel will sort by int since Fuel is enum. Should switch to custom value object.
- Running all integration test together will fail. Need to run each test class separately. Will be fixed later.

## ToDo

- Introduce event sourcing for audit
- Set fluent validation in application layer for better documentation in swagger.
- Bug fixes and code improvements.
