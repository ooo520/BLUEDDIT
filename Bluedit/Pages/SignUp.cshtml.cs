using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

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
			if (ModelState.IsValid && !string.IsNullOrWhiteSpace(Pseudo) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Mail))
			{
                try
                {
					if (_userRepository.GetByName(Pseudo) == null);
					{ 
						Dbo.User usr = await _userRepository.SignUp(Pseudo, Password, Mail);
						if (usr != null) 
						{ 
							return RedirectToPage("/login");
						}
					}
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured");  // A remplacer par des logs
                }
			}
			return RedirectToPage("/signup");
		}
    }
}
