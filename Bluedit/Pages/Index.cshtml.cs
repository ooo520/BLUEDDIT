using bluedit.DataAccess;
using bluedit.DataAccess.Interfaces;
using bluedit.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bluedit.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly DataAccess.Interfaces.IUserRepository _userRepository;
        public IEnumerable<Category> categories;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(ICategoryRepository categoryRepository, DataAccess.Interfaces.IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public string username => _httpContextAccessor.HttpContext.Request.Cookies["username"];
        public string userpass => _httpContextAccessor.HttpContext.Request.Cookies["userpass"];

        public async Task<IActionResult> OnGet()
        {
            //if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userpass) || (_userRepository.GetByName(username)?.Password != userpass))
            //{
            //    return RedirectToPage("/login");
            //}

            categories = await _categoryRepository.Read();
            return Page();
        }
    }
}
