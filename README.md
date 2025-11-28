# Store.Architectures.Modular-Monolith
Modular Monolith Architecture in .NET

Refactoring [Vertical-Slice repository](https://github.com/doruz/Store.Architectures.Vertical-Slice) into Modular Monolith.

Modules:
- Products
- Orders
- Shopping Cart
- Shared

Module project:
- V1
  - Store.{Module}.Domain
  - Store.{Module}.Business
  - Store.{Module}.Infrastructure
  - Store.{Module}.Contracts
- V2
  - Store.{Module}
    - Domain
    - Business
  - Store.{Module}.Infrastructure
  - Store.{Module}.Contracts