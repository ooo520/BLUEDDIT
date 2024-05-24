
using AutoMapper;

namespace bluedit.DataAccess
{
    public class UserRepository : Repository<EfModels.User, Dbo.User>, Interfaces.IUserRepository
    {
        public UserRepository(EfModels.BlueditContext context, ILogger<UserRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
		}
		public async Task<Dbo.User> GetByNameAsync(string name)
		{
			throw new NotImplementedException();
		}

	}
}
