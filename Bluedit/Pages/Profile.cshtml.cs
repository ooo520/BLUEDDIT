using bluedit.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bluedit.Pages
{
    public class ProfileModel : PageModel
    {
        private IUserRepository _userRepository { get; set; }
		[BindProperty(SupportsGet = true)]
        public string? UserName { get; set; }
        public Dbo.User Blueditor {  get; set; }
        public ProfileModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (UserName == null)
            {
                return BadRequest();
            }

            Dbo.User user = _userRepository.GetByName(UserName);

            if (user == null)
            {
                return NotFound();
            }

            Blueditor = user;

            return Page();
        }
        public IActionResult OnPostDisconnect()
        {
            //Console.WriteLine("aaaaaaaaaaaaaa");
            HttpContext.Response.Cookies.Delete("username");
            return RedirectToPage("/");
        }
    }
}
