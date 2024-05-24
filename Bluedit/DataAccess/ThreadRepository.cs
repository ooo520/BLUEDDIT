﻿using AutoMapper;
using bluedit.DataAccess.Interfaces;

namespace bluedit.DataAccess
{
	public class ThreadRepository(
		EfModels.BlueditContext context,
		ILogger<ThreadRepository> logger,
		IMapper mapper
	) :
		Repository<EfModels.Thread, Dbo.Thread>(context, logger, mapper),
		IThreadRepository
	{
		public ICollection<Dbo.Thread> GetByCategory(long categoryId, GetThreadsOptions? options = null)
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
					threads = threads
						.OrderBy(thread => thread.Answers
							.OrderBy(b => b.CreationDate)
							.First().CreationDate
						);
				}
				else if (options.sort == ThreadSort.TOP)
				{
					throw new Exception("Can't sort by popularity yet.");
				}
			}

			return _mapper.Map<List<Dbo.Thread>>(threads.ToList());
		}
		public Dbo.Thread? GetById(long id)
		{
			// TODO: why is thread nullable here?
			return _mapper.Map<Dbo.Thread>(_context.Threads.FirstOrDefault(thread => thread.Id == id, null));
		}
	}
}
