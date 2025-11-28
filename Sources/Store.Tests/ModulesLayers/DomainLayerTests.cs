namespace Store.Tests.ModulesLayers;

public class DomainLayerTests
{
    [Fact]
    public void DomainTypesThatArePublic_Should_ResideInFixedNamespace()
    {
        var result = SolutionTypes.Domain
            .That()
            .ArePublic()
            .Should()
            .ResideInFixedNamespace(SolutionNamespaces.DomainPattern)
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void DomainTypes_Should_NotHavePropertiesSettersExposedExternally()
    {
        var result = SolutionTypes.Domain
            .Should()
            .HaveAllPropertiesWithoutPublicSetters()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void DomainValues_Should_BePublicImmutableRecords()
    {
        var result = SolutionTypes.Domain
            .That()
            .AreRecords()
            .Should()
            .BePublic().And().HaveAllPropertiesWithInitOnly()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void InMemoryRepositories_Should_HaveNameEndingWithRepository()
    {
        var result = SolutionTypes.Domain
            .That()
            .ImplementInterfacesEndingWithName("Repository")
            .Should()
            .HaveNameEndingWith("Repository")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}