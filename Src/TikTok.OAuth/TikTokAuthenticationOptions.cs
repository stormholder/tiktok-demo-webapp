using Microsoft.AspNetCore.Authentication.OAuth;

namespace TikTok.OAuth;

public class TikTokAuthenticationOptions : OAuthOptions
{
    public TikTokAuthenticationOptions()
    {
        AuthorizationEndpoint = TikTokAuthenticationDefaults.AuthorizationEndpoint;
        TokenEndpoint = TikTokAuthenticationDefaults.TokenEndpoint;
        UserInformationEndpoint = TikTokAuthenticationDefaults.UserInformationEndpoint;
        CallbackPath = TikTokAuthenticationDefaults.CallbackPath;

        Scope.Add("user.info.basic");
        Scope.Add("video.list");
        Scope.Add("video.upload");
    }
}