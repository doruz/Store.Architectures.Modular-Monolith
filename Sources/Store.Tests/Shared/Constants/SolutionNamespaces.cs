namespace Store.Tests;

internal static class SolutionNamespaces
{
    private const string Base = "Store";
    private const string ModulesPattern = "(Orders|Products|ShoppingCarts)";

    public const string ContractsName = "Contracts";
    public const string DomainName = "Domain";
    public const string BusinessName = "Business";
    public const string InfrastructureName = "Infrastructure";


    public const string DomainPattern = $"{Base}.{ModulesPattern}.{DomainName}";
    public const string BusinessPattern = $"{Base}.{ModulesPattern}.{BusinessName}";
    public const string InfrastructurePattern = $"{Base}.{ModulesPattern}.{InfrastructureName}";

    public static ModuleNamespaces Orders { get; } = new("Orders");
    public static ModuleNamespaces Products { get; } = new("Products");
    public static ModuleNamespaces ShoppingCarts { get; } = new("ShoppingCarts");
    public static IReadOnlyList<ModuleNamespaces> Modules { get; } = [Orders, Products, ShoppingCarts];

    public static string[] Domain => Modules.Select(m => m.Domain).ToArray();
    public static string[] Business => Modules.Select(m => m.Business).ToArray();
    public static string[] Infrastructure => [Shared.Infrastructure, ..Modules.Select(m => m.Infrastructure)];

    public static class Shared
    {
        public const string All = $"{Base}.Shared";

        public const string Infrastructure = $"{All}.{InfrastructureName}";
    }

    public static class Presentation
    {
        public const string All = $"{Base}.Presentation";

        public const string Api = $"{All}.Api";
    }

    public record ModuleNamespaces(string Module)
    {
        public string Domain => $"{Base}.{Module}.{DomainName}";
        public string Business => $"{Base}.{Module}.{BusinessName}";
        public string Infrastructure => $"{Base}.{Module}.{InfrastructureName}";

        public string[] All => [Domain, Business, Infrastructure];
    }
}