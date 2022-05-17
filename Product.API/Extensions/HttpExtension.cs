using System.Text.Json;

namespace Product.API.Extensions;

public static class HttpExtension
{
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage responseMessage)
    {
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new ApplicationException($"Something went wrong calling the API: {responseMessage.ReasonPhrase}");
        }
        var strData = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonSerializer.Deserialize<T>(strData);
    }
    public static IApplicationBuilder useAuthMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<AuthMiddleware>();
        return app;
    }
}