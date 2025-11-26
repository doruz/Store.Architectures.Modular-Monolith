using System.Net.Http.Json;

namespace Store.Presentation.Api.IntegrationTests;

internal static class HttpResponseMessageExtensions
{
    public static async Task<T> ContentAsAsync<T>(this HttpResponseMessage response)
        => (await response.Content.ReadFromJsonAsync<T>())!;

    public static async Task<T> ContentAsAsync<T>(this Task<HttpResponseMessage> response)
        => (await (await response).Content.ReadFromJsonAsync<T>())!;

    public static async Task<HttpResponseMessage> EnsureIsSuccess(this Task<HttpResponseMessage> response)
    {
        var httpResponse = await response;

        if (httpResponse.IsSuccessStatusCode is false)
        {
            throw new HttpRequestException(await httpResponse.Content.ReadAsStringAsync(), null, httpResponse.StatusCode);
        }

        return httpResponse;
    }
}