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
    }
}
