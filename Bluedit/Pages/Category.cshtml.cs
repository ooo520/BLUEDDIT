using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bluedit.Pages
{

	public class CategoryModel : PageModel
	{
		public ICollection<Dbo.Thread> Threads { get; set; }
		[BindProperty(SupportsGet = true)]
		public string? CategoryName { get; set; }
		public Dbo.Category? Category { get; set; }

		private readonly DataAccess.Interfaces.IThreadRepository _threadRepository;
		private readonly DataAccess.Interfaces.ICategoryRepository _categoryRepository;

		public CategoryModel(
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

			Category = _categoryRepository.GetByName(CategoryName);
			if (Category == null)
			{
				return NotFound();
			}

			// TODO: pagination ?
			Threads = _threadRepository.GetByCategory(Category.Id, null);

			return Page();
		}
	}
}
