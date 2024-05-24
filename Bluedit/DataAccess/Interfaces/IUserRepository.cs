namespace Bluedit.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<EfModels.User, Dbo.User>
    {
		public Task<Dbo.User> GetByNameAsync(string name);
	}
}
