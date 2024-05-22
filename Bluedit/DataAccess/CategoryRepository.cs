using AutoMapper;

namespace Bluedit.DataAccess
{
    public class CategoryRepository : Repository<EfModels.Category, Dbo.Category>, Interfaces.ICategoryRepository
    {
        public CategoryRepository(EfModels.BlueditContext context, ILogger<CategoryRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
        }
    }
}
