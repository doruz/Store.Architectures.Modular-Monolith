# Store.Architectures.Modular-Monolith
Modular Monolith Architecture in .NET


Modules:
	- Products
	- Orders
	- Shopping Cart

Module project:
	- Store.{module}.Domain
	- Store.{module}.Business
	- Store.{module}.Public/Contracts/Client


ArchTests
	- Store.{module}.Domain/Business to have fixed namespaces