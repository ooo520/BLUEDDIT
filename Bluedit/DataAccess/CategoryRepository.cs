using AutoMapper;
using Bluedit.Dbo;

namespace Bluedit.DataAccess
{
    public class CategoryRepository : Repository<EfModels.Category, Dbo.Category>, Interfaces.ICategoryRepository
    {
        public CategoryRepository(EfModels.BlueditContext context, ILogger<CategoryRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

        public List<Category> GetCategories(string title)
        {
            return _mapper.Map<List<Dbo.Category>>(_context.Categories.Where(x=> x.Title == title).ToList());
        }
    }
}
