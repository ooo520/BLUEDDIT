using bluedit.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bluedit.Pages
{
    public class NewThreadRequest
    {
        public readonly string? title = null;
		public readonly string? content = null;
	}

    public class NewThreadModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? CategoryName { get; set; }
		public Category? Category { get; set; }

		private readonly DataAccess.Interfaces.IThreadRepository _threadRepository;
		private readonly DataAccess.Interfaces.ICategoryRepository _categoryRepository;

		public NewThreadModel(
			DataAccess.Interfaces.IThreadRepository threadRepository,
			DataAccess.Interfaces.ICategoryRepository categoryRepository
		)
		{
			_threadRepository = threadRepository;
			_categoryRepository = categoryRepository;
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

        public async Task<IActionResult> OnPostAsync([FromBody] NewThreadRequest request)
        {
			if (request.title == null || request.content == null) {
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
				Title = request.title,
			};
			Dbo.Answer rootAnswer = new()
			{
				Content = request.content,
				CreationDate = DateTime.Now,
				Thread = newThread
			};
			//newThread.RootAnswer = rootAnswer;

			Dbo.Thread createdThread = await _threadRepository.Create(newThread);
			return RedirectToPage($"/b/{CategoryName}/t/{createdThread.Id}");
		}
    }
}
