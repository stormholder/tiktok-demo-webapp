using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication;
using TikTok.OAuth;

namespace Microsoft.Extensions.DependencyInjection;

public static class TikTokAuthenticationExtensions
{
    public static AuthenticationBuilder AddTikTok([NotNull] this AuthenticationBuilder builder)
    {
        return builder.AddTikTok(TikTokAuthenticationDefaults.AuthenticationScheme, _ => {});
    }

    public static AuthenticationBuilder AddTikTok(
        [NotNull] this AuthenticationBuilder builder,
        [NotNull] Action<TikTokAuthenticationOptions> configuration)
    {
        return builder.AddTikTok(TikTokAuthenticationDefaults.AuthenticationScheme, configuration);
    }

    public static AuthenticationBuilder AddTikTok(
        [NotNull] this AuthenticationBuilder builder,
        [NotNull] string scheme,
        [NotNull] Action<TikTokAuthenticationOptions> configuration)
    {
        return builder.AddTikTok(scheme, TikTokAuthenticationDefaults.DisplayName, configuration);
    }

    public static AuthenticationBuilder AddTikTok(
        [NotNull] this AuthenticationBuilder builder,
        [NotNull] string scheme,
        [NotNull] string caption,
        [NotNull] Action<TikTokAuthenticationOptions> configuration)
    {
        return builder.AddOAuth<TikTokAuthenticationOptions, TikTokAuthenticationHandler>(scheme, caption, configuration);
    }
}