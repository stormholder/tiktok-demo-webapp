using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
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

        if (!string.IsNullOrEmpty(Options.ClientSecret))
        {
            challengeUrl = QueryHelpers.AddQueryString(challengeUrl, "client_key", Options.ClientSecret);
        }
        if (Options.Scope.Any())
        {
            challengeUrl = QueryHelpers.AddQueryString(challengeUrl, "scope", String.Join(",", Options.Scope));
        }

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

        internal static async Task EmailAddressErrorAsync(ILogger logger, HttpResponseMessage response, CancellationToken cancellationToken)
        {
            EmailAddressError(
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

        [LoggerMessage(2, LogLevel.Warning, "An error occurred while retrieving the email address associated with the logged in user: the remote server returned a {Status} response with the following payload: {Headers} {Body}.")]
        private static partial void EmailAddressError(
            ILogger logger,
            System.Net.HttpStatusCode status,
            string headers,
            string body);
    }
}