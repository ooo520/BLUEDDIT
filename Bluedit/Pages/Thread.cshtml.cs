using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bluedit.Pages
{
    public class ThreadModel : PageModel
    {

		[BindProperty(SupportsGet = true)]
		public string? CategoryName { get; set; }
		[BindProperty(SupportsGet = true)]
		public long ThreadId { get; set; }
		public Dbo.Thread Thread { get; set; }

		private readonly DataAccess.Interfaces.IThreadRepository _threadRepository;
		private readonly DataAccess.Interfaces.IAnswerRepository _answerRepository;

		public ThreadModel(
			DataAccess.Interfaces.IThreadRepository threadRepository,
			DataAccess.Interfaces.IAnswerRepository answerRepository
		)
		{
			_threadRepository = threadRepository;
			_answerRepository = answerRepository;
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

			return Page();
        }
    }
}
