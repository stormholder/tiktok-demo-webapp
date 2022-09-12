using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TikTok.Web.Pages;

public class TOSModel : PageModel
{
    private readonly ILogger<TOSModel> _logger;

    public TOSModel(ILogger<TOSModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

