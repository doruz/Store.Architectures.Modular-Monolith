namespace Store.Tests;

internal static class SolutionNamespaces
{
    private const string Modules = "(Orders|Products|ShoppingCarts)";

    public const string Domain = $"Store.{Modules}.Domain";
    public const string Business = $"Store.{Modules}.Business";
    public const string Infrastructure = $"Store.{Modules}.Infrastructure";

    public static class Core
    {
        public const string All = "Store.Core";

        public const string Shared = $"{All}.Shared";
        public const string Domain = $"{All}.Domain";
        public const string Business = $"{All}.Business";
    }

    public static class InfrastructureOld
    {
        public const string All = "Store.Infrastructure";

        public const string Persistence = $"{All}.Persistence";
    }

    public static class Presentation
    {
        public const string All = "Store.Presentation";

        public const string Api = $"{All}.Api";
    }
}