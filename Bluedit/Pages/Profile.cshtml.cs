using bluedit.DataAccess.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;

namespace bluedit.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly IUserRepository _userRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		[BindProperty(SupportsGet = true)]
        public string? UserName { get; set; }
        public Dbo.User Blueditor {  get; set; }
        public bool isCurrentUser = false;

        public ProfileModel(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
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

			if (user.Name == _httpContextAccessor.HttpContext.Request.Cookies["username"]
                && user.Password == _httpContextAccessor.HttpContext.Request.Cookies["userpass"])
            {
                isCurrentUser = true;
            }

			Blueditor = user;

            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
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

			if (user.Name != _httpContextAccessor.HttpContext.Request.Cookies["username"]
				|| user.Password != _httpContextAccessor.HttpContext.Request.Cookies["userpass"])
			{
				return Unauthorized();  // using unauthorized instead of Forbid() because it would show unhandled exception otherwise
			}

			var newDescription = Request.Form["Description"];
            if (newDescription.ToString().Length > 256)
            {
                return BadRequest();
            }

            user.Description = newDescription;
            await _userRepository.Update(user);

            return Redirect(_httpContextAccessor.HttpContext.Request.GetEncodedPathAndQuery());
		}

        public IActionResult OnPostDisconnect()
        {
            HttpContext.Response.Cookies.Delete("username");
			HttpContext.Response.Cookies.Delete("userpass");
			return RedirectToPage("/Index");
        }
    }
}
