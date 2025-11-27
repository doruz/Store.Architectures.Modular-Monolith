using MediatR;

namespace Store.Tests.Solution;

public class CqrsPatternTests
{
    private const string Command = "Command";
    private const string CommandHandler = "CommandHandler";

    private const string Query = "Query";
    private const string QueryHandler = "QueryHandler";

    private const string Event = "Event";
    private const string EventHandler = "Handler";

    [Fact]
    public void Cqrs_Should_FollowNamingConventions()
    {
        var policy = Policy
            .Define("CQRS", "Should follow naming conventions")

            .For(SolutionTypes.All)

            .ThatImplementInterfaceShouldHaveNameEndingWith(typeof(IRequest), Command)
            .ThatImplementInterfaceShouldHaveNameEndingWith(typeof(IRequestHandler<>), CommandHandler)

            .ThatImplementInterfaceShouldHaveNameEndingWith(typeof(IRequest<>), Command, Query)
            .ThatImplementInterfaceShouldHaveNameEndingWith(typeof(IRequestHandler<,>), CommandHandler, QueryHandler)

            .ThatImplementInterfaceShouldHaveNameEndingWith(typeof(INotification), Event)
            .ThatImplementInterfaceShouldHaveNameEndingWith(typeof(INotificationHandler<>), EventHandler);

        policy.Evaluate().ShouldBeSuccessful();
    }

    [Fact]
    public void CqrsModels_Should_BePublicImmutableRecords()
    {
        var policy = Policy
            .Define("CQRS", "Should follow implementation conventions")

            .For(SolutionTypes.All)

            .ThatImplementInterfaceShouldBePublicImmutableRecords(typeof(IRequest))
            .ThatImplementInterfaceShouldBePublicImmutableRecords(typeof(IRequest<>))
            .ThatImplementInterfaceShouldBePublicImmutableRecords(typeof(INotification))
            .ThatHaveNameEndingShouldBePublicImmutableRecords("Result", "Model");

        policy.Evaluate().ShouldBeSuccessful();
    }


    [Fact]
    public void CqrsHandlers_Should_FollowImplementationConventions()
    {
        var policy = Policy
            .Define("CQRS", "Should follow implementation conventions")

            .For(SolutionTypes.All)

            .ThatImplementInterfaceShouldNotBeExposed(typeof(IRequestHandler<>))
            .ThatImplementInterfaceShouldNotBeExposed(typeof(IRequestHandler<,>))
            .ThatImplementInterfaceShouldNotBeExposed(typeof(INotificationHandler<>));

        policy.Evaluate().ShouldBeSuccessful();
    }

    [Fact]
    public void Cqrs_Should_NotExposeDomainTypes()
    {
        var policy = Policy
            .Define("CQRS", "Should not expose business domain types")

            .For(SolutionTypes.All)

            .Add(types => types
                    .That()
                    .ImplementInterface(typeof(IRequest))
                    .Or().ImplementInterface(typeof(IRequest<>))
                    .Or().ImplementInterface(typeof(INotification))
                    .Or().HaveNameMatching($"({Query}|{Command})Result$")
                    .Or().ImplementInterface(typeof(IRequestHandler<>))
                    .Or().ImplementInterface(typeof(IRequestHandler<,>))
                    .Or().ImplementInterface(typeof(INotificationHandler<>))
                    .ShouldNot()
                    .HavePropertiesWithTypesFrom("Domain"),
                "Commands & Queries & Events",
                "Types should not expose domain types through their properties."
            )

            .Add(types => types
                    .That()
                    .ImplementInterface(typeof(IRequest))
                    .Or().ImplementInterface(typeof(IRequest<>))
                    .Or().ImplementInterface(typeof(INotification))
                    .Or().ImplementInterface(typeof(IRequestHandler<>))
                    .Or().ImplementInterface(typeof(IRequestHandler<,>))
                    .Or().ImplementInterface(typeof(INotificationHandler<>))
                    .ShouldNot()
                    .UseTypesOnPublicMethodsFrom("Domain"),
                "Commands & Queries & Events",
                "Types should not expose domain types through their public methods."
            );

        policy.Evaluate().ShouldBeSuccessful();
    }
}