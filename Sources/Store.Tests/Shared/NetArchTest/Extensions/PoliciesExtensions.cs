namespace Store.Tests;

internal static class PoliciesExtensions
{
    internal static PolicyDefinition ThatImplementInterfaceShouldHaveNameEndingWith(this PolicyDefinition definition, Type interfaceType, params string[] namesSuffix)
    {
        var nameEndingPattern = $"{string.Join("|", namesSuffix)}$";
        var namesDescription = string.Join(" & ", namesSuffix);

        return definition.Add(types => types
                .That()
                .ResideInNamespaceStartingWith("Store").And().ImplementInterface(interfaceType)
                .Should()
                .HaveNameMatching(nameEndingPattern),

            namesDescription,
            $"Names of types that implement {interfaceType.FullName} should end with {namesDescription}"
        );
    }

    internal static PolicyDefinition ThatImplementInterfaceShouldBePublicImmutableRecords(this PolicyDefinition definition, Type interfaceType)
    {
        return definition.Add(types => types
                .That()
                .ResideInNamespaceStartingWith("Store").And().ImplementInterface(interfaceType)
                .Should()
                .BePublic().And().BeRecords().And().HaveAllPropertiesWithInitOnly(),

            interfaceType.FullName,
            $"Types that implement {interfaceType.FullName} should be public immutable records"
        );
    }

    internal static PolicyDefinition ThatHaveNameEndingShouldBePublicImmutableRecords(this PolicyDefinition definition, params string[] namesSuffix)
    {
        var nameEndingPattern = $"{string.Join("|", namesSuffix)}$";
        var namesDescription = string.Join(" & ", namesSuffix);

        return definition.Add(types => types
                .That()
                .ResideInNamespaceStartingWith("Store").And().HaveNameMatching(nameEndingPattern)
                .Should()
                .BePublic().And().BeRecords().And().HaveAllPropertiesWithInitOnly(),

            namesDescription,
            $"Types that have name ending with '{namesDescription}' should be public immutable records"
        );
    }

    internal static PolicyDefinition ThatImplementInterfaceShouldNotBeExposed(this PolicyDefinition definition, Type interfaceType)
    {
        return definition.Add(types => types
                .That()
                .ResideInNamespaceStartingWith("Store").And().ImplementInterface(interfaceType)
                .Should()
                .NotBePublic().And().BeSealed().And().BeClasses().And().NotBeRecords(),

            interfaceType.FullName,
            $"Types that implement {interfaceType.FullName} should be non-public sealed classes"
        );
    }
}