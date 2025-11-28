namespace Store.Tests.ModulesLayers;

public class InfrastructureLayerTests
{
    [Fact]
    public void InfrastructureTypes_Should_BeExposedOnlyThroughAbstractions()
    {
        var result = SolutionTypes.Infrastructure
            .That()
            .DoNotHaveNameEndingWith("InfrastructureLayer")
            .ShouldNot()
            .BePublic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AllRepositories_Should_HaveNameEndingWithRepository()
    {
        var result = SolutionTypes.Infrastructure
            .That()
            .ImplementInterfacesEndingWithName("Repository")
            .Should()
            .HaveNameEndingWith("Repository")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void CosmosPersistence_Should_ResideInNamespaceAndFollowNamingConvention()
    {
        var result = SolutionTypes.Infrastructure
            .That()
            .HaveDependencyOn("Microsoft.Azure.Cosmos")
            .Should()
            .ResideInFixedNamespace($"{SolutionNamespaces.InfrastructurePattern}.Cosmos")
            .And().HaveNameStartingWith("Cosmos")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}