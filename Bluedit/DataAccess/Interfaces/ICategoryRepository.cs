namespace bluedit.DataAccess.Interfaces
{
	public interface ICategoryRepository : IRepository<EfModels.Category, Dbo.Category>
	{
		public Dbo.Category? GetByName(string name);

		public Dbo.Category? GetById(long id);

		List<Dbo.Category> GetCategories(string search);
	}
}
