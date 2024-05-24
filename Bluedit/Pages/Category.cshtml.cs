using bluedit.DataAccess.EfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace bluedit.Pages
{

	public class CategoryModel : PageModel
	{
		public ICollection<Dbo.Thread> Threads { get; set; }
		[BindProperty(SupportsGet = true)]
		public string? CategoryName { get; set; }
		public Dbo.Category? Category { get; set; }

		public Dictionary<long, Dbo.Answer> ThreadIdToRootAnswerMap { get; set; } = new();

		public readonly DataAccess.Interfaces.IAnswerRepository _answerRepository;
		public readonly DataAccess.Interfaces.ICategoryRepository _categoryRepository;
		public readonly DataAccess.Interfaces.IThreadRepository _threadRepository;

		public CategoryModel(
			DataAccess.Interfaces.IAnswerRepository answerRepository,
			DataAccess.Interfaces.ICategoryRepository categoryRepository,
			DataAccess.Interfaces.IThreadRepository threadRepository
		)
		{
			_answerRepository = answerRepository;
			_categoryRepository = categoryRepository;
			_threadRepository = threadRepository;
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

			Threads = _threadRepository.GetByCategory(Category.Id, null);

			foreach (Dbo.Thread thread in Threads) {
				Dbo.Answer rootAnswer = _answerRepository.GetRootAnswerOfThread(thread.Id);
				if (rootAnswer == null)
				{
					throw new NullReferenceException($"GetRootAnswerOfThread returned null for thread with id {thread.Id}.");
				}
				ThreadIdToRootAnswerMap.Add(thread.Id, rootAnswer);
			}

			return Page();
		}
	}
}
