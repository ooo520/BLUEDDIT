using AutoMapper;
using System.Diagnostics;

namespace bluedit.DataAccess
{
	public class AnswerRepository : Repository<EfModels.Answer, Dbo.Answer>, Interfaces.IAnswerRepository
	{
		public AnswerRepository(EfModels.BlueditContext context, ILogger<AnswerRepository> logger, IMapper mapper) : base(context, logger, mapper)
		{

		}

		public ICollection<Dbo.Answer> GetByThread(long threadId)
		{
			return _mapper.Map<ICollection<Dbo.Answer>>(_context.Answers.Where(answer => answer.ThreadId == threadId));
		}

		public Dbo.Answer? GetRootAnswerOfThread(long threadId)
		{
			EfModels.Answer? answer = _context.Answers
					.OrderBy(answer => answer.CreationDate)
					.FirstOrDefault(answer => answer.ThreadId == threadId);
			if (answer == null)
			{
				return null;
			}
			return _mapper.Map<Dbo.Answer>(answer);
		}
	}
}
