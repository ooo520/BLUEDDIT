
using AutoMapper;

namespace Bluedit.DataAccess
{
    public class UserRepository : Repository<EfModels.User, Dbo.User>, Interfaces.IUserRepository
    {
        public UserRepository(EfModels.BlueditContext context, ILogger<UserRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

    }
}
