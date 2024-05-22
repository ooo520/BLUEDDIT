namespace Bluedit.DataAccess.Interfaces
{
    public interface ICategoryRepository : IRepository<EfModels.Category, Dbo.Category>
    {
            List<Dbo.Category> GetCategories(string title);
    }
}
