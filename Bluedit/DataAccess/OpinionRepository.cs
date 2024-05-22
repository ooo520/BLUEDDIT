using AutoMapper;

namespace Bluedit.DataAccess
{
    public class OpinionRepository : Repository<EfModels.Opinion, Dbo.Opinion>, Interfaces.IOpinionRepository
    {
        public OpinionRepository(EfModels.BlueditContext context, ILogger<OpinionRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
        }
    }
}
