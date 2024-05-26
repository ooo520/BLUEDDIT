using AutoMapper;

namespace bluedit.DataAccess
{
    public class OpinionRepository : Repository<EfModels.Opinion, Dbo.Opinion>, Interfaces.IOpinionRepository
    {
        public OpinionRepository(EfModels.BlueditContext context, ILogger<OpinionRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

        public long GetLikesCountForAnswer(long answerId)
        {
            return _context.Opinions.Where(o => o.AnswerId == answerId).ToList().Aggregate(0, (total, next) => next.Like ? total + 1 : total - 1);
        }

        public Dbo.Opinion? GetUserOpinionOnAnswer(long userId, long answerId)
        {
			EfModels.Opinion? opinion = _context.Opinions.FirstOrDefault(o => o.AnswerId == answerId && o.AuthorId == userId);
			if (opinion == null)
			{
				return null;
			}
			return _mapper.Map<Dbo.Opinion>(opinion);
		}
    }
}
