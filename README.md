# MoveIt

Full example to consume REST Web API 2 From a MVC Client with full SOLID implementation

Case Analysis

- A company transfer people stuff between places.
- They need to have their next Web Application to ease the operation for customers.
- Through this application, clients should be able to get proposals and place offers.

Architecture
- SQL DB generated from Code First with given data set.
- Data Access Layer to expose CRUD functionality to Web API “Entity Framework”.
- Web API MVC 2 to expose HTTP functionality to all clients.
  - SOLID.
  - Repositories pattern.
  - AutoFac IoC.
  - Automapper for DTO.
  - Place Proposals
  - View Proposals.
  - Accept Offers.
  - Google OAuth.
  - Facebook OAuth.
  - Identity.
  - BootStrap , glyphicons Font Icons
