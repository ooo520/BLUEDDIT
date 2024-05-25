using bluedit.Dbo;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Web;

namespace bluedit.Pages
{

    public class NewThreadModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string CategoryName { get; set; }
		public Category? Category { get; set; }

		private readonly DataAccess.Interfaces.IThreadRepository _threadRepository;
		private readonly DataAccess.Interfaces.ICategoryRepository _categoryRepository;
		private readonly DataAccess.Interfaces.IAnswerRepository _answerRepository;
		private readonly DataAccess.Interfaces.IUserRepository _userRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public NewThreadModel(
			DataAccess.Interfaces.IThreadRepository threadRepository,
			DataAccess.Interfaces.ICategoryRepository categoryRepository,
			DataAccess.Interfaces.IAnswerRepository answerRepository,
			DataAccess.Interfaces.IUserRepository userRepository,
			IHttpContextAccessor httpContextAccessor
		)
		{
			_threadRepository = threadRepository;
			_categoryRepository = categoryRepository;
			_answerRepository = answerRepository;
			_userRepository = userRepository;
			_httpContextAccessor = httpContextAccessor;
		}

		public string username => _httpContextAccessor.HttpContext.Request.Cookies["username"];
		public string userpass => _httpContextAccessor.HttpContext.Request.Cookies["userpass"];

		public IActionResult OnGet()
		{
			if (!IsLoggedIn())
			{
				return Redirect("/login/" + HttpUtility.UrlEncode(_httpContextAccessor.HttpContext.Request.GetEncodedPathAndQuery()));
			}

			Category = _categoryRepository.GetByName(CategoryName);
			if (Category == null)
			{
				return NotFound();
			}

			return Page();
		}

        public async Task<IActionResult> OnPostAsync()
		{
			if (!IsLoggedIn())
			{
				return Redirect("/login/" + HttpUtility.UrlEncode(_httpContextAccessor.HttpContext.Request.GetEncodedPathAndQuery()));
			}

			var title = Request.Form["Title"];
			var content = Request.Form["Content"];

			if (title == "" || content == "") {
				return BadRequest();
			}

			if (CategoryName == null)
			{
				return NotFound();
			}

			Category = _categoryRepository.GetByName(CategoryName);
			if (Category == null)
			{
				return NotFound();
			}

			Dbo.Thread newThread = new() { 
				CategoryId = Category.Id,
				Title = title,
			};
            Dbo.Thread createdThread = await _threadRepository.Create(newThread);

			long userId = _userRepository.GetByName(username).Id; // should not be null
			Dbo.Answer rootAnswer = new()
			{
				Content = content,
				CreationDate = DateTime.Now,
				ThreadId = createdThread.Id,
				UserId = userId
			};
			await _answerRepository.Create(rootAnswer);

			return Redirect($"/b/{CategoryName}/t/{createdThread.Id}");
		}

		private bool IsLoggedIn()
		{
			return !(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userpass) || (_userRepository.GetByName(username)?.Password != userpass));
		}
    }
}
