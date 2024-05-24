namespace bluedit.DataAccess.Interfaces
{
	public interface ICategoryRepository : IRepository<EfModels.Category, Dbo.Category>
	{
		public Dbo.Category? GetByName(string name);

		List<Dbo.Category> GetCategories(string title);
	}
}
