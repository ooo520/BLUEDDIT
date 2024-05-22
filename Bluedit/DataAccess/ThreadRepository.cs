using AutoMapper;

namespace Bluedit.DataAccess
{
    public class ThreadRepository : Repository<EfModels.Thread, Dbo.Thread>, Interfaces.IThreadRepository
    {
        public ThreadRepository(EfModels.BlueditContext context, ILogger<ThreadRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
        }
    }
}
