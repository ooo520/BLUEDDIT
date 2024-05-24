namespace Bluedit.DataAccess.Interfaces
{
	public enum ThreadSort
	{
		NEW,
		TOP
	}

	public class GetThreadsOptions
	{
		public ThreadSort sort = ThreadSort.NEW;
		public String? titleFilter;
	}

    public interface IThreadRepository : IRepository<EfModels.Thread, Dbo.Thread>
    {
		public Dbo.Thread GetById(long id);
		public ICollection<Dbo.Thread> GetByCategory(long categoryId, GetThreadsOptions? options);

	}
}
