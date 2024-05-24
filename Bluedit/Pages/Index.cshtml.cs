using Bluedit.DataAccess.Interfaces;
using Bluedit.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bluedit.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        public IEnumerable<Category> categories;

        public IndexModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            categories = await _categoryRepository.Read();
            return Page();
        }
    }
}
