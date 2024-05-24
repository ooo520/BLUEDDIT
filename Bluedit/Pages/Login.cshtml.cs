using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace bluedit.Pages
{
    public class LoginModel : PageModel
    {

		private readonly DataAccess.Interfaces.IUserRepository _userRepository;
		public LoginModel(
			DataAccess.Interfaces.IUserRepository userRepository
		)
		{
			_userRepository = userRepository;
		}

		public void OnGet()
        {
        }
		[BindProperty]
		[Required]
		public string Pseudo { get; set; }

		[BindProperty]
		[Required]
		public string Password { get; set; }

		public ActionResult OnPost()
		{
			if (ModelState.IsValid)
			{
				Console.WriteLine(_userRepository.SignIn(Pseudo, Password).Id);
			}
			return RedirectToPage("/login");
		}
	}
}
