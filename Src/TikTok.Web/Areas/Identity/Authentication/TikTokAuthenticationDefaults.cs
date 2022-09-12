namespace TikTok.Web.Areas.Identity.Authentication;

public static class TikTokAuthenticationDefaults
{
    public const string AuthenticationScheme = "TikTok";
    public static readonly string DisplayName = "TikTok";
    public static readonly string Issuer = "TikTok";
    public static readonly string AuthorizationEndpoint = "https://www.tiktok.com/auth/authorize/";
    public static readonly string TokenEndpoint = "https://open-api.tiktok.com/oauth/access_token/";
    public static readonly string CallbackPath = new PathString("/auth/signin-tiktok");
}