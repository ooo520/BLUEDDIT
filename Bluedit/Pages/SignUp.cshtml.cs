using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;

namespace bluedit.Pages
{
    public class SignUpModel : PageModel
    {
		[BindProperty(SupportsGet = true)]
		public string? redirectUrl { get; set; }

		public void OnGet()
        {
        }

		private readonly DataAccess.Interfaces.IUserRepository _userRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public SignUpModel(
			DataAccess.Interfaces.IUserRepository userRepository,
			IHttpContextAccessor httpContextAccessor
		)
		{
			_userRepository = userRepository;
			_httpContextAccessor = httpContextAccessor;
		}

		[BindProperty]
		[Required]
		public string Pseudo { get; set; }

		[BindProperty]
		[Required]
		public string Password { get; set; }

		[BindProperty]
		[Required]
		public string Mail { get; set; }
		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid && !string.IsNullOrWhiteSpace(Pseudo) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Mail))
			{
                try
                {
					if (_userRepository.GetByName(Pseudo) == null);
					{ 
						Dbo.User usr = await _userRepository.SignUp(Pseudo, Password, Mail);
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured");  // A remplacer par des logs
                }
			}
			return RedirectToPage("/signup/" + redirectUrl);
		}
    }
}
