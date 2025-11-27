namespace Store.Tests.Solution;

public class GeneralArchitectureTests
{
    [Fact]
    public void TypeExtensions_Should_BeStatic()
    {
        var result = SolutionTypes.All
            .That()
            .ResideInNamespaceMatching("Store").And().HaveNameEndingWith("Extensions")
            .Should()
            .BeStatic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void Interfaces_Should_BePublicAndFollowNamingConvention()
    {
        var result = SolutionTypes.All
            .That()
            .ResideInNamespaceMatching("Store").And().AreInterfaces()
            .Should()
            .BePublic().And().HaveNameStartingWith("I")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}