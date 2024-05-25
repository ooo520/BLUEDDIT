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

		public ThreadModel(
			DataAccess.Interfaces.IThreadRepository threadRepository,
			DataAccess.Interfaces.IAnswerRepository answerRepository,
			DataAccess.Interfaces.IUserRepository userRepository,
			DataAccess.Interfaces.ICategoryRepository categoryRepository
		)
		{
			_threadRepository = threadRepository;
			_answerRepository = answerRepository;
			_userRepository = userRepository;
			_categoryRepository = categoryRepository;
		}

		public async Task<IActionResult> OnGetAsync()
        {
			if (ThreadId == null)
			{
				return NotFound();
			}
			Console.WriteLine(CategoryName);
			Console.WriteLine(ThreadId);
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
			if (category.Name !=  CategoryName)
			{
				return BadRequest();
			}
			Answers = _answerRepository.GetByThread(ThreadId).OrderBy(a => a.CreationDate).ToList();
			foreach (var answer in Answers)
			{
				answer.User = _userRepository.GetById(answer.UserId)!;
			}

			return Page();
        }
    }
}
