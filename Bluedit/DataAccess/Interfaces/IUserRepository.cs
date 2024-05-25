namespace bluedit.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<EfModels.User, Dbo.User>
    {
		public Dbo.User? GetByName(string name);
        public Dbo.User? GetById(long id);
		public  Task<Dbo.User?> SignUp(string pseudo, string password, string mail);
		public Dbo.User? SignIn(string pseudo, string password);
		public string Hash(string password);
	}
}
