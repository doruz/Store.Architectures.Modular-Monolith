namespace Store.Tests;

internal static class SolutionNamespaces
{
    private const string Base = "Store";
    private const string ModulesPattern = "(Orders|Products|ShoppingCarts)";

    public const string ContractsName = "Contracts";
    public const string DomainName = "Domain";
    public const string BusinessName = "Business";
    public const string InfrastructureName = "Infrastructure";


    public const string ContractsPattern = $"{Base}.{ModulesPattern}.{ContractsName}";
    public const string DomainPattern = $"{Base}.{ModulesPattern}.{DomainName}";
    public const string BusinessPattern = $"{Base}.{ModulesPattern}.{BusinessName}";
    public const string InfrastructurePattern = $"{Base}.{ModulesPattern}.{InfrastructureName}";

    public static ModuleNamespaces Orders { get; } = new("Orders");
    public static ModuleNamespaces Products { get; } = new("Products");
    public static ModuleNamespaces ShoppingCarts { get; } = new("ShoppingCarts");
    public static IReadOnlyList<ModuleNamespaces> Modules { get; } = [Orders, Products, ShoppingCarts];

    public static string[] Domain => Modules.Select(m => m.Domain).ToArray();
    public static string[] Business => Modules.Select(m => m.Business).ToArray();
    public static string[] Infrastructure => Modules.Select(m => m.Infrastructure).ToArray();

    public static class Presentation
    {
        public const string All = "{Base}.Presentation";

        public const string Api = $"{All}.Api";
    }

    public class ModuleNamespaces(string moduleName)
    {
        public string Contracts => $"{Base}.{moduleName}.{ContractsName}";
        public string Domain => $"{Base}.{moduleName}.{DomainName}";
        public string Business => $"{Base}.{moduleName}.{BusinessName}";
        public string Infrastructure => $"{Base}.{moduleName}.{InfrastructureName}";
    }
}