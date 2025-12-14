using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Response;
using System.Net.Http;
using System.Runtime.ExceptionServices;

namespace StarRailDamage.Source.Web.Request
{
    public static class HttpRequestMessageBuilderExtension
    {
        public static async ValueTask<FinalizedResponse<TResult>> SendAsync<TResult>(this HttpRequestMessageBuilder builder, HttpClient httpClient, CancellationToken cancellationToken)
        {
            return await builder.SendAsync<TResult>(httpClient, HttpCompletionOption.ResponseContentRead, cancellationToken);
        }

        public static async ValueTask<FinalizedResponse<TResult>> SendAsync<TResult>(this HttpRequestMessageBuilder builder, HttpClient httpClient, HttpCompletionOption httpCompletionOption, CancellationToken cancellation)
        {
            using HttpContext HttpContext = new()
            {
                HttpClient = httpClient,
                CompletionOption = httpCompletionOption,
                Cancellation = cancellation
            };
            await SendAsync(builder, HttpContext).ConfigureAwait(false);
            if (HttpContext.Exception.IsNull() && HttpContext.Response.IsNotNull())
            {
                try
                {
                    return new FinalizedResponse<TResult>(HttpContext.Response.Headers, await builder.HttpContentSerializer.DeserializeAsync<TResult>(HttpContext.Response.Content, cancellation).ConfigureAwait(false));
                }
                catch (Exception Exception)
                {
                    HttpContext.Exception = ExceptionDispatchInfo.Capture(Exception);
                }
            }
            return new FinalizedResponse<TResult>(HttpContext.Response?.Headers, HttpContext.Exception);
        }

        public static async ValueTask SendAsync(this HttpRequestMessageBuilder builder, HttpContext context)
        {
            try
            {
                context.Request = builder.HttpRequestMessage;
                context.Response = await context.HttpClient.SendAsync(context.Request, context.CompletionOption, context.Cancellation).ConfigureAwait(false);
                context.Response.EnsureSuccessStatusCode();
            }
            catch (Exception Exception)
            {
                context.Exception = ExceptionDispatchInfo.Capture(Exception);
            }
        }
    }
}