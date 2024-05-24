namespace bluedit.DataAccess.Interfaces
{
    public interface IAnswerRepository : IRepository<EfModels.Answer, Dbo.Answer>
    {
        public ICollection<Dbo.Answer> GetByThread(long threadId);
		public Dbo.Answer GetRootAnswerOfThread(long threadId);
	}
}
