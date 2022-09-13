using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TikTok.OAuth;

public partial class TikTokAuthenticationHandler : OAuthHandler<TikTokAuthenticationOptions>
{
    public TikTokAuthenticationHandler(
        [NotNull] IOptionsMonitor<TikTokAuthenticationOptions> options,
        [NotNull] ILoggerFactory logger,
        [NotNull] UrlEncoder encoder,
        [NotNull] ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    /// <inheritdoc />
    protected override string BuildChallengeUrl(
        [NotNull] AuthenticationProperties properties,
        [NotNull] string redirectUri)
    {
        string challengeUrl = base.BuildChallengeUrl(properties, redirectUri);
        var uri = new Uri(challengeUrl);
        var baseUri = uri.GetComponents(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped);
        var query = QueryHelpers.ParseQuery(uri.Query);
        query.Remove("client_id");
        query["scope"] = String.Join(",", Options.Scope);
        var qb = new QueryBuilder(query);
        qb.Add("client_key", Options.ClientId);
        challengeUrl = baseUri + qb.ToQueryString();

        return challengeUrl;
    }

    private static partial class Log
    {
        internal static async Task UserProfileErrorAsync(ILogger logger, HttpResponseMessage response, CancellationToken cancellationToken)
        {
            UserProfileError(
                logger,
                response.StatusCode,
                response.Headers.ToString(),
                await response.Content.ReadAsStringAsync(cancellationToken));
        }

        [LoggerMessage(1, LogLevel.Error, "An error occurred while retrieving the user profile: the remote server returned a {Status} response with the following payload: {Headers} {Body}.")]
        private static partial void UserProfileError(
            ILogger logger,
            System.Net.HttpStatusCode status,
            string headers,
            string body);
    }
}