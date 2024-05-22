using AutoMapper;

namespace Bluedit.DataAccess
{
    public class AnswerRepository : Repository<EfModels.Answer, Dbo.Answer>, Interfaces.IAnswerRepository
    {
        public AnswerRepository(EfModels.BlueditContext context, ILogger<AnswerRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
        }
    }
}
