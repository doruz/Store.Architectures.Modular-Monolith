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

            .TypesImplementingInterfaceHaveNameEndingWith(typeof(IRequest), Command)
            .TypesImplementingInterfaceHaveNameEndingWith(typeof(IRequestHandler<>), CommandHandler)

            .TypesImplementingInterfaceHaveNameEndingWith(typeof(IRequest<>), Command, Query)
            .TypesImplementingInterfaceHaveNameEndingWith(typeof(IRequestHandler<,>), CommandHandler, QueryHandler)

            .TypesImplementingInterfaceHaveNameEndingWith(typeof(INotification), Event)
            .TypesImplementingInterfaceHaveNameEndingWith(typeof(INotificationHandler<>), EventHandler);

        policy.Evaluate().ShouldBeSuccessful();
    }


    [Fact]
    public void Cqrs_Should_FollowImplementationConventions()
    {
        var policy = Policy
            .Define("CQRS", "Should follow implementation conventions")

            .For(SolutionTypes.All)

            .Add(types => types
                    .That()
                    .ImplementInterface(typeof(IRequest)).Or().ImplementInterface(typeof(IRequest<>)).Or().ImplementInterface(typeof(INotification))
                    .Or()
                    .HaveNameMatching($"({Query}|{Command})Result$")
                    .Should()
                    .BePublic().And().BeRecords().And().HaveAllPropertiesWithInitOnly(),
                "Commands & Queries",
                $"Types that implement {typeof(IRequest).FullName} should be public immutable records"
            )

            .Add(types => types
                    .That()
                    .ImplementInterface(typeof(IRequestHandler<>)).Or().ImplementInterface(typeof(IRequestHandler<,>)).Or().ImplementInterface(typeof(INotificationHandler<>))
                    .Should()
                    .NotBePublic().And().BeSealed().And().BeClasses().And().NotBeRecords(),
                "Commands & Queries Handlers",
                $"Types that implement {typeof(IRequestHandler<>).FullName} should be non-public sealed classes"
            );

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
                    .ImplementInterface(typeof(IRequest)).Or().ImplementInterface(typeof(IRequest<>)).Or().ImplementInterface(typeof(INotification))
                    .Or()
                    .HaveNameMatching($"({Query}|{Command})Result$")
                    .Or()
                    .ImplementInterface(typeof(IRequestHandler<>)).Or().ImplementInterface(typeof(IRequestHandler<,>))
                    .ShouldNot()
                    .HavePropertiesWithTypesFrom("Domain"),
                "Commands & Queries",
                "Types should not expose domain types through their properties."
            )

            .Add(types => types
                    .That()
                    .ImplementInterface(typeof(IRequest)).Or().ImplementInterface(typeof(IRequest<>)).Or().ImplementInterface(typeof(INotification))
                    .Or()
                    .ImplementInterface(typeof(IRequestHandler<>)).Or().ImplementInterface(typeof(IRequestHandler<,>)).Or().ImplementInterface(typeof(INotificationHandler<>))
                    .ShouldNot()
                    .UseTypesOnPublicMethodsFrom("Domain"),
                "Commands & Queries",
                "Types should not expose domain types through their public methods."
            );

        policy.Evaluate().ShouldBeSuccessful();
    }
}