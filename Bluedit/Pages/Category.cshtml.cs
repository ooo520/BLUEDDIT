using bluedit.DataAccess;
using bluedit.DataAccess.EfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public List<SelectListItem> SortOptions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "ASC", Text = "Nouveaux" },
            new SelectListItem { Value = "DESC", Text = "Ancien" },
            new SelectListItem { Value = "TOP", Text = "Populaire" },

            new SelectListItem { Value = "A-Z", Text = "A - Z" },
            new SelectListItem { Value = "Z-A", Text = "Z - A" },
        };

        [BindProperty(SupportsGet = true)]
        public string Sorting { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        public readonly DataAccess.Interfaces.IAnswerRepository _answerRepository;
		public readonly DataAccess.Interfaces.ICategoryRepository _categoryRepository;
		public readonly DataAccess.Interfaces.IThreadRepository _threadRepository;
		public readonly DataAccess.Interfaces.IOpinionRepository _opinionRepository;
		public readonly DataAccess.Interfaces.IUserRepository _userRepository;
		public CategoryModel(
			DataAccess.Interfaces.IAnswerRepository answerRepository,
			DataAccess.Interfaces.ICategoryRepository categoryRepository,
			DataAccess.Interfaces.IThreadRepository threadRepository,
			DataAccess.Interfaces.IOpinionRepository opinionRepository,
			DataAccess.Interfaces.IUserRepository userRepository
		)
		{
			_answerRepository = answerRepository;
			_categoryRepository = categoryRepository;
			_threadRepository = threadRepository;
			_opinionRepository = opinionRepository;
			_userRepository = userRepository;
		}

        public IActionResult OnGet()
		{
            if (CategoryName == null) return NotFound();
            Category = _categoryRepository.GetByName(CategoryName);
            if (Category == null) return NotFound();
            Threads = _threadRepository.GetByCategory(Category.Id, null);

            foreach (Dbo.Thread thread in Threads)
            {
                Dbo.Answer rootAnswer = _answerRepository.GetRootAnswerOfThread(thread.Id);
                if (rootAnswer == null)
                {
                    throw new NullReferenceException($"GetRootAnswerOfThread returned null for thread with id {thread.Id}.");
                }
                ThreadIdToRootAnswerMap.Add(thread.Id, rootAnswer);
            }
            if (Threads != null)
            {
                if (!string.IsNullOrEmpty(SearchQuery))
                    Threads = Threads.Where((t) => t.Title.Contains(SearchQuery)).ToList();
                switch (Sorting)
                {
                    case "ASC":
                        Threads = Threads.OrderBy((t) => ThreadIdToRootAnswerMap[t.Id].CreationDate).ToList();
                        break;
                    case "DESC":
                        Threads = Threads.OrderByDescending((t) => ThreadIdToRootAnswerMap[t.Id].CreationDate).ToList();
                        break;
                    case "A-Z":
                        Threads = Threads.OrderBy((t) => t.Title).ToList();
                        break;
                    case "Z-A":
                        Threads = Threads.OrderByDescending((t) => t.Title).ToList();
                        break;
                    case "TOP":
                        Threads = Threads.OrderBy((t) => _opinionRepository.GetLikesCountForAnswer(ThreadIdToRootAnswerMap[t.Id].Id)).ToList();
                        break;
                    default:
                        break;
                }
            }
            return Page();
		}
	}
}
