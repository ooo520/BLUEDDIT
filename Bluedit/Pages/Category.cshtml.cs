using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Bluedit.Pages
{

	public class CategoryModel : PageModel
	{
		public List<Dbo.Thread> Threads { get; set; }
		[BindProperty(SupportsGet = true)]
		public string? CategoryName { get; set; }

		private readonly DataAccess.Interfaces.IThreadRepository _threadRepository;

		public CategoryModel(DataAccess.Interfaces.IThreadRepository threadRepository)
		{
			_threadRepository = threadRepository;
		}

		public async Task<IActionResult> OnGetAsync()
        {
			if (CategoryName == null)
			{
				return NotFound();
			}

			Debug.WriteLine(CategoryName);

			// TODO: pagination ?
			//Threads = (await _threadRepository.Read()).ToList();
			Threads = [];

			return Page();
		}
    }
}
