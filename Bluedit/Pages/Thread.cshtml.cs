using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bluedit.Pages
{
    public class ThreadModel : PageModel
    {
        public ICollection<Dbo.Answer> Answers { get; set; }

		[BindProperty(SupportsGet = true)]
		public string? CategoryName { get; set; }
		[BindProperty]
		public long ThreadId { get; set; }
		public Dbo.Thread? Thread { get; set; }

		private readonly DataAccess.Interfaces.IThreadRepository _threadRepository;
		private readonly DataAccess.Interfaces.IAnswerRepository _answerRepository;
		private readonly DataAccess.Interfaces.IUserRepository _userRepository;

		public ThreadModel(
			DataAccess.Interfaces.IThreadRepository threadRepository,
			DataAccess.Interfaces.IAnswerRepository answerRepository,
			DataAccess.Interfaces.IUserRepository userRepository
		)
		{
			_threadRepository = threadRepository;
			_answerRepository = answerRepository;
			_userRepository = userRepository;
		}

		public async Task<IActionResult> OnGetAsync()
        {
			if (ThreadId == null)
			{
				return NotFound();
			}
			Thread = _threadRepository.GetById(ThreadId);
			if (Thread == null)
			{
				return NotFound();
			}
			Answers = _answerRepository.GetByThread(ThreadId);
			foreach (var answer in Answers)
			{
				answer.User = _userRepository.GetById(answer.UserId);
			}
			return Page();
        }
    }
}
