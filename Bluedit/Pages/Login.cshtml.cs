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
            _httpContextAccessor = httpContextAccessor;
        }

		[BindProperty(SupportsGet = true)]
		public string? redirectUrl { get; set; }

		public void OnGet()
        {
            // TODO: maybe prevent this if logged in (and redirect to profile page)
        }

		public IActionResult OnPost()
		{
            string Pseudo = Request.Form["Pseudo"];
            string Password = Request.Form["Password"];

			if (ModelState.IsValid && !string.IsNullOrWhiteSpace(Pseudo) && !string.IsNullOrWhiteSpace(Password))
			{
                try
                {
                    Dbo.User usr = _userRepository.SignIn(Pseudo, Password);
                    if (usr != null)
                    {
                        _httpContextAccessor.HttpContext.Response.Cookies.Append("username", usr.Name);
                        _httpContextAccessor.HttpContext.Response.Cookies.Append("userpass", usr.Password);
                        if (redirectUrl != null)
                        {
                            return Redirect(HttpUtility.UrlDecode(redirectUrl));
                        }
                        return RedirectToPage("/Index");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured");  // A remplacer par des logs
                }
            }
			return Redirect("/login/" + redirectUrl);
		}
	}
}
