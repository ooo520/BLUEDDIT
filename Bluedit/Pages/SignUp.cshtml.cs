using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace bluedit.Pages
{
    public class SignUpModel : PageModel
    {
        public void OnGet()
        {
        }

		private readonly DataAccess.Interfaces.IUserRepository _userRepository;
		public SignUpModel(
			DataAccess.Interfaces.IUserRepository userRepository
		)
		{
			_userRepository = userRepository;
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
			if (ModelState.IsValid)
			{
				Console.WriteLine((await _userRepository.SignUp(Pseudo, Password, Mail))?.Id);
			}
			return RedirectToPage("/signup");
		}
	}
}
