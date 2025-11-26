namespace Store.Shared;

// TODO: to see if these could be changed with 'System.Linq.AsyncEnumerable'
public static class TaskExtensions
{
    public static async Task<IEnumerable<TResult>> SelectAsync<T, TResult>(this Task<IEnumerable<T>> values, Func<T, TResult> mapper)
        => (await values).Select(mapper);

    public static async Task<List<T>> ToListAsync<T>(this IEnumerable<Task<T>> valuesTasks)
    {
        var result = new List<T>();

        foreach (var valueTask in valuesTasks)
        {
            result.Add(await valueTask);
        }

        return result;
    }

    public static async Task<List<T>> ForEachAsync<T>(this IEnumerable<T> items, Func<T, Task> action)
    {
        var itemsList = items.ToList();

        foreach (T item in itemsList)
        {
            await action(item);
        }

        return itemsList;
    }
}

