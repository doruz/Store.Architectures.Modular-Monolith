namespace Store.Tests;

internal static class PoliciesExtensions
{
    internal static PolicyDefinition TypesImplementingInterfaceHaveNameEndingWith(this PolicyDefinition definition, Type interfaceType, params string[] namesSuffix)
    {
        var nameEndingPattern = $"{string.Join("|", namesSuffix)}$";
        var namesDescription = string.Join(" & ", namesSuffix);

        return definition.Add(types => types
                .That()
                .ImplementInterface(interfaceType).And().ResideInNamespaceStartingWith("Store")
                .Should()
                .HaveNameMatching(nameEndingPattern),

            namesDescription,
            $"Names of types that implement {interfaceType.FullName} should end with {namesDescription}"
        );
    }
}