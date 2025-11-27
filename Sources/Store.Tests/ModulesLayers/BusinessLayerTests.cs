namespace Store.Tests.ModulesLayers;

public class BusinessLayerTests
{
    [Fact]
    public void BusinessTypes_Should_ResideInFixedNamespace()
    {
        var result = SolutionTypes.Business
            .That()
            .ArePublic()
            .Should()
            .ResideInFixedNamespace(SolutionNamespaces.Business)
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessServices_Should_BePublicSealedClasses()
    {
        var result = SolutionTypes.Business
            .That()
            .HaveNameEndingWith("Service")
            .Should()
            .BePublic().And().BeSealed().And().BeClasses().And().NotBeRecords()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessServices_Should_NotExposeExtraLayerOfAbstraction()
    {
        var result = SolutionTypes.Business
            .That()
            .HaveNameEndingWith("Service")
            .ShouldNot()
            .ImplementInterfaces()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessModels_Should_BePublicImmutableRecords()
    {
        var result = SolutionTypes.Business
            .That()
            .HaveNameEndingWith("Model")
            .Should()
            .BePublic().And().BeRecords().And().HaveAllPropertiesWithInitOnly()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessLayer_Should_NotExposeDomainTypes()
    {
        var policy = Policy
            .Define("Business Layer", "Should not expose business domain types")

            .For(SolutionTypes.Business)

            .Add(types => types
                    .That()
                    .HaveNameEndingWith("Model")
                    .ShouldNot()
                    .HavePropertiesWithTypesFrom("Domain"),
                "Business models",
                "Models should not expose domain types through their properties."
            )

            .Add(types => types
                    .That()
                    .HaveNameEndingWith("Service")
                    .ShouldNot()
                    .UseTypesOnPublicMethodsFrom("Domain"),
                "Business services",
                "Services should not expose domain types through their public methods."
            );

        policy.Evaluate().ShouldBeSuccessful();
    }
}