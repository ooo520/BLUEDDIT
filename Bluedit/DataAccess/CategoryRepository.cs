using AutoMapper;
using bluedit.Dbo;

namespace bluedit.DataAccess
{
    public class CategoryRepository : Repository<EfModels.Category, Dbo.Category>, Interfaces.ICategoryRepository
    {
        public CategoryRepository(EfModels.BlueditContext context, ILogger<CategoryRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

        public Category? GetByName(string name)
        {
            var c = _context.Categories.FirstOrDefault(x => x.Name == name);
            if (c == null)
            {
                return null;
            }

            return _mapper.Map<Category>(c);
		}

        public Category? GetById(long id)
        {
            var c = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (c == null)
            {
                return null;
            }

            return _mapper.Map<Category>(c);
        }

        public List<Category> GetCategories(string search)
        {
            return _mapper.Map<List<Category>>(_context.Categories.Where(x=> x.Title.Contains(search)).ToList());
        }
    }
}
