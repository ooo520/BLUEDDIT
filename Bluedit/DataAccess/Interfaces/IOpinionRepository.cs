namespace bluedit.DataAccess.Interfaces
{
    public interface IOpinionRepository : IRepository<EfModels.Opinion, Dbo.Opinion>
    {
		public long GetLikesCountForAnswer(long answerId);
		public Dbo.Opinion? GetUserOpinionOnAnswer(long userId, long answerId);

	}
}
