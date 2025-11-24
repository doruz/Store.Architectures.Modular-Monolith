using System.Reflection;

namespace Store.Shared.Infrastructure;

public static class SharedInfrastructureLayer
{
    public static Assembly Assembly => typeof(SharedInfrastructureLayer).Assembly;

    // TODO: to register CosmosClient
}