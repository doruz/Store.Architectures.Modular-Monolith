namespace Store.Tests.Solution;

public class OnionArchitectureTests
{
    [Fact]
    public void ContractsLayer_Should_NotHaveDependenciesOnLayersAbove()
    {
        var result = SolutionTypes.All
            .That()
            .ResideInNamespaceContaining(SolutionNamespaces.ContractsName)
            .Should()
            .NotHaveDependencyOnAny
            (
                [
                    ..SolutionNamespaces.Domain,
                    ..SolutionNamespaces.Business,
                    ..SolutionNamespaces.Infrastructure,
                    SolutionNamespaces.Presentation.All
                ]
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void DomainLayers_Should_NotHaveDependenciesOnLayersAbove()
    {
        var result = SolutionTypes.All
            .That()
            .ResideInNamespaceContaining(SolutionNamespaces.DomainName)
            .Should()
            .NotHaveDependencyOnAny
            (
                [
                    ..SolutionNamespaces.Business,
                    ..SolutionNamespaces.Infrastructure,
                    SolutionNamespaces.Presentation.All
                ]
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessLayer_Should_NotHaveDependenciesOnLayersAbove()
    {
        var result = SolutionTypes.All
            .That()
            .ResideInNamespaceContaining(SolutionNamespaces.BusinessName)
            .Should()
            .NotHaveDependencyOnAny
            (
                [
                    ..SolutionNamespaces.Infrastructure,
                    SolutionNamespaces.Presentation.All
                ]
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void InfrastructureLayer_Should_NotHaveDependenciesOnLayersAboveBusiness()
    {
        var result = SolutionTypes.All
            .That()
            .ResideInNamespaceContaining(SolutionNamespaces.InfrastructureName)
            .Should()
            .NotHaveDependencyOnAny
            (
                SolutionNamespaces.Presentation.All
            )
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}