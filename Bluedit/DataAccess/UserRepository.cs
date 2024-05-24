
using AutoMapper;

namespace bluedit.DataAccess
{
    public class UserRepository : Repository<EfModels.User, Dbo.User>, Interfaces.IUserRepository
    {
        public UserRepository(EfModels.BlueditContext context, ILogger<UserRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
		}
		public Dbo.User? GetByName(string name)
		{
			EfModels.User? user = _context.Users.FirstOrDefault(x => x.Name == name);
			if (user == null)
			{
				return null;
			}
			return _mapper.Map<Dbo.User>(user);
		}

	}
}
