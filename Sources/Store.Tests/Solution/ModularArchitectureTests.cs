namespace Store.Tests.Solution;

public class ModularArchitectureTests
{
    [Fact]
    public void Shared_Should_NotHaveDependenciesOnAnyModule()
    {
        var result = SolutionTypes.All
            .That()
            .ResideInNamespaceContaining(SolutionNamespaces.Shared.All)
            .Should()
            .NotHaveDependencyOnAny
            (
                [
                    ..SolutionNamespaces.Orders.All,
                    ..SolutionNamespaces.Products.All,
                    ..SolutionNamespaces.ShoppingCarts.All
                ]
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void Orders_Should_NotHaveDependenciesOnOtherModules()
    {
        var result = SolutionTypes.All
            .That()
            .ResideInNamespaceContaining(SolutionNamespaces.Orders.Module)
            .Should()
            .NotHaveDependencyOnAny
            (
                [
                    ..SolutionNamespaces.Products.All,
                    ..SolutionNamespaces.ShoppingCarts.All
                ]
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void Products_Should_NotHaveDependenciesOnOtherModules()
    {
        var result = SolutionTypes.All
            .That()
            .ResideInNamespaceContaining(SolutionNamespaces.Products.Module)
            .Should()
            .NotHaveDependencyOnAny
            (
                [
                    ..SolutionNamespaces.Orders.All,
                    ..SolutionNamespaces.ShoppingCarts.All
                ]
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void ShoppingCarts_Should_NotHaveDependenciesOnOtherModules()
    {
        var result = SolutionTypes.All
            .That()
            .ResideInNamespaceContaining(SolutionNamespaces.ShoppingCarts.Module)
            .Should()
            .NotHaveDependencyOnAny
            (
                [
                    ..SolutionNamespaces.Orders.All,
                    ..SolutionNamespaces.Products.All
                ]
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}