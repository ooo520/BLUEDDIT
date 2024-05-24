using AutoMapper;
using Bluedit.DataAccess.Interfaces;

namespace Bluedit.DataAccess
{
	public class ThreadRepository(
		EfModels.BlueditContext context,
		ILogger<ThreadRepository> logger,
		IMapper mapper
	) :
		Repository<EfModels.Thread, Dbo.Thread>(context, logger, mapper),
		IThreadRepository
	{
		public ICollection<Dbo.Thread> GetThreadsByCategory(long categoryId, GetThreadsOptions? options)
		{
			IQueryable<EfModels.Thread> threads = _context.Threads.Where(thread => thread.CategoryId == categoryId);

			if (options != null)
			{
				if (options.titleFilter != null)
				{
					threads = threads.Where(thread => thread.Title.Contains(options.titleFilter));
				}

				if (options.sort == ThreadSort.NEW)
				{
					threads = threads.OrderBy(thread => thread.RootAnswer.CreationDate);
				}
				else if (options.sort == ThreadSort.TOP)
				{
					throw new Exception("Can't sort by popularity yet.");
				}
			}

			return _mapper.Map<List<Dbo.Thread>>(threads);
		}
	}
}
