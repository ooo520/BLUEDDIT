using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace bluedit.Pages
{
    public class LoginModel : PageModel
    {

		private readonly DataAccess.Interfaces.IUserRepository _userRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(DataAccess.Interfaces.IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
		{
			_userRepository = userRepository;
			Password = "";
			Pseudo = "";
            _httpContextAccessor = httpContextAccessor;
        }

		[BindProperty(SupportsGet = true)]
		public string redirectUrl { get; set; }

		public void OnGet()
        {
            Console.WriteLine(redirectUrl);
            Console.WriteLine(HttpUtility.UrlDecode(redirectUrl));
        }

		[BindProperty]
		[Required]
		public string Pseudo { get; set; }

		[BindProperty]
		[Required]
		public string Password { get; set; }

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid && !string.IsNullOrWhiteSpace(Pseudo) && !string.IsNullOrWhiteSpace(Password))
			{
                try
                {
                    Dbo.User usr = _userRepository.SignIn(Pseudo, Password);
                    if (usr != null)
                    {
                        HttpContext.Response.Cookies.Append("username", usr.Name);
                        HttpContext.Response.Cookies.Append("userpass", usr.Password);
                        return Redirect(HttpUtility.UrlDecode(redirectUrl));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured");  // A remplacer par des logs
                }
            }
			return RedirectToPage("/login/" + redirectUrl);
		}
	}
}
