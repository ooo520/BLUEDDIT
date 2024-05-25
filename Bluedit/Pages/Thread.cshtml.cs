using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bluedit.Pages
{
    public class ThreadModel : PageModel
	{
		public ICollection<Dbo.Answer> Answers { get; set; }

		[BindProperty(SupportsGet = true)]
		public string? CategoryName { get; set; }
		[BindProperty(SupportsGet = true)]
		public long ThreadId { get; set; }
		public Dbo.Thread Thread { get; set; }

		private readonly DataAccess.Interfaces.IThreadRepository _threadRepository;
		private readonly DataAccess.Interfaces.IAnswerRepository _answerRepository;
		private readonly DataAccess.Interfaces.IUserRepository _userRepository;
		private readonly DataAccess.Interfaces.ICategoryRepository _categoryRepository;
		private readonly DataAccess.Interfaces.IOpinionRepository _opinionRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ThreadModel(
			DataAccess.Interfaces.IThreadRepository threadRepository,
			DataAccess.Interfaces.IAnswerRepository answerRepository,
			DataAccess.Interfaces.IUserRepository userRepository,
			DataAccess.Interfaces.ICategoryRepository categoryRepository,
			DataAccess.Interfaces.IOpinionRepository opinionRepository,
			IHttpContextAccessor httpContextAccessor
		)
		{
			_threadRepository = threadRepository;
			_answerRepository = answerRepository;
			_userRepository = userRepository;
			_categoryRepository = categoryRepository;
			_opinionRepository = opinionRepository;
			_httpContextAccessor = httpContextAccessor;
		}

		public string username => _httpContextAccessor.HttpContext.Request.Cookies["username"];
		public string userpass => _httpContextAccessor.HttpContext.Request.Cookies["userpass"];

		public async Task<IActionResult> OnGetAsync()
        {
			if (!ThreadIsCorrect())
			{
				return NotFound();
			}

			Thread = _threadRepository.GetById(ThreadId);
			if (Thread == null)
			{
				return NotFound();
			}

			var category = _categoryRepository.GetById(Thread.CategoryId);
			if (category == null)
			{
				return NotFound();
			}
			if (category.Name != CategoryName)
			{
				return BadRequest();
			}

			Answers = _answerRepository.GetByThread(ThreadId).OrderBy(a => a.CreationDate).ToList();
			foreach (var answer in Answers)
			{
				answer.User = _userRepository.GetById(answer.UserId)!;
				answer.Likes = _opinionRepository.GetLikesCountForAnswer(answer.Id);
			}

			return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
			if (!IsLoggedIn())
			{
				return RedirectToPage("/login");
			}

			var comment = Request.Form["Comment"];

            if (comment == "")
            {
                return BadRequest();
            }

            if (!ThreadIsCorrect())
            {
                return NotFound();
            }

			long userId = _userRepository.GetByName(username).Id; // should not be null
			Dbo.Answer newAnswer = new()
            {
                Content = comment,
                CreationDate = DateTime.Now,
                ThreadId = ThreadId,
                UserId = userId
            };
            await _answerRepository.Create(newAnswer);

            return Redirect($"/b/{CategoryName}/t/{ThreadId}");
        }

        private bool ThreadIsCorrect()
        {
            if (ThreadId == 0 || CategoryName == "")
            {
                return false;
            }
            Thread = _threadRepository.GetById(ThreadId);
            if (Thread == null)
            {
                return false;
            }
            var category = _categoryRepository.GetById(Thread.CategoryId);
            if (category == null)
            {
                return false;
            }
            if (category.Name != CategoryName)
            {
                return false;
            }

            return true;
        }

		private bool IsLoggedIn()
		{
			return !(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userpass) || (_userRepository.GetByName(username)?.Password != userpass));
		}
	}
}
