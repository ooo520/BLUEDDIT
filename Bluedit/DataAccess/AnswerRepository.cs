using AutoMapper;

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
	}
}
