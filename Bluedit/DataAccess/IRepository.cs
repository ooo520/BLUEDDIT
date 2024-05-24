using System.Collections.Generic;
using System.Threading.Tasks;

namespace bluedit.DataAccess
{
    public interface IRepository<DBEntity, ModelEntity>
    {
        Task<ModelEntity> Create(ModelEntity entity);
        Task<IEnumerable<ModelEntity>> Read(string includeTables = "");
        Task<ModelEntity> Update(ModelEntity entity);
        Task<bool> Delete(long idEntity);
    }
}