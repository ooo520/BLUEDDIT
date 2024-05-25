using bluedit.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bluedit.Pages
{

    public class NewThreadModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? CategoryName { get; set; }
		public Category? Category { get; set; }

		private readonly DataAccess.Interfaces.IThreadRepository _threadRepository;
		private readonly DataAccess.Interfaces.ICategoryRepository _categoryRepository;
		private readonly DataAccess.Interfaces.IAnswerRepository _answerRepository;

		public NewThreadModel(
			DataAccess.Interfaces.IThreadRepository threadRepository,
			DataAccess.Interfaces.ICategoryRepository categoryRepository,
			DataAccess.Interfaces.IAnswerRepository answerRepository
		)
		{
			_threadRepository = threadRepository;
			_categoryRepository = categoryRepository;
			_answerRepository = answerRepository;
		}


		public async Task<IActionResult> OnGetAsync()
		{
			if (CategoryName == null)
			{
				return NotFound();
			}

			// Category = _categoryRepository.GetByName(CategoryName);

			return Page();
		}

        public async Task<IActionResult> OnPostAsync()
        {
			var title = Request.Form["Title"];
			var content = Request.Form["Content"];
			Console.WriteLine(title);
			Console.WriteLine(content);
			if (title == "" || content == "") {
				Console.WriteLine("TITLE OR CONTENT IS EMPTY AAAAAAAAAAAAAAAA");
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

			Dbo.Answer rootAnswer = new()
			{
				Content = content,
				CreationDate = DateTime.Now,
				ThreadId = createdThread.Id,
				UserId = 1 // TODO: get user id
			};
			await _answerRepository.Create(rootAnswer);

			return Redirect($"/b/{CategoryName}/t/{createdThread.Id}");
		}
    }
}
