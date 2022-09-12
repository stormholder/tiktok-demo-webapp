using Microsoft.AspNetCore.Authentication.OAuth;

namespace TikTok.Web.Areas.Identity.Authentication;

public class TikTokAuthenticationOptions : OAuthOptions
{
    public TikTokAuthenticationOptions()
    {
        AuthorizationEndpoint = TikTokAuthenticationDefaults.AuthorizationEndpoint;
        TokenEndpoint = TikTokAuthenticationDefaults.TokenEndpoint;
        // UserInformationEndpoint = TikTokAuthenticationDefaults.UserInformationEndpoint;
        CallbackPath = new PathString("/auth/signin-tiktok");
        Scope.Add("user.info.basic");
        Scope.Add("video.list");
        Scope.Add("video.upload");
    }
}