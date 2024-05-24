namespace bluedit.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<EfModels.User, Dbo.User>
    {
		public Dbo.User GetByName(string name);
        public Dbo.User GetById(long id);
	}
}
