using TikTok.Web.Areas.Identity.Authentication;

namespace TikTok.Web.StartupExtensions;

public static class AuthExtension
{
    public static IServiceCollection AddTikTokAuth(
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = "TikTok";
        })
        .AddOAuth(TikTokAuthenticationDefaults.AuthenticationScheme, tiktokOptions => {
            tiktokOptions.AuthorizationEndpoint = TikTokAuthenticationDefaults.AuthorizationEndpoint;
            tiktokOptions.CallbackPath = TikTokAuthenticationDefaults.CallbackPath;
            tiktokOptions.TokenEndpoint = TikTokAuthenticationDefaults.TokenEndpoint;
            tiktokOptions.ClientId = configuration["Authentication:TikTok:ClientId"];
            tiktokOptions.ClientSecret = configuration["Authentication:TikTok:ClientSecret"];
            tiktokOptions.Scope.Add("user.info.basic");
            tiktokOptions.Scope.Add("video.list");
            tiktokOptions.Scope.Add("video.upload");
        });
        return services;
    }

    public static IApplicationBuilder UseTikTokAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}
