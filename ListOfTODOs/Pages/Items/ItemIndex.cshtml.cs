using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ListOfTODOs.Pages
{
    public class ItemIndexModel : PageModel
    {
        private readonly ILogger<ItemIndexModel> _logger;

        public ItemIndexModel(ILogger<ItemIndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
