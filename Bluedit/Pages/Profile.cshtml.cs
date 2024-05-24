using Bluedit.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bluedit.Pages
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

            Dbo.User user = await _userRepository.GetByNameAsync(UserName);

            if (user == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
