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
        .AddOAuth("TikTok", tiktokOptions => {
            tiktokOptions.ClientId = configuration["Authentication:TikTok:ClientId"];
            tiktokOptions.ClientSecret = configuration["Authentication:TikTok:ClientSecret"];
            tiktokOptions.SaveTokens = true;
            tiktokOptions.Scope.Add("user.info.basic");
            tiktokOptions.Scope.Add("video.list");
        });
        return services;
    }

    public static IApplicationBuilder UseTikTokAuth(this IApplicationBuilder app)
    {
        return app;
    }
}
