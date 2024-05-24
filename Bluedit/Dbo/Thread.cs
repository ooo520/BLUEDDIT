namespace bluedit.Dbo
{
    public class Thread : IObjectWithId
    {
        public long Id { get; set; }

        public string Title { get; set; } = null!;

        public long CategoryId { get; set; }

        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

        public virtual Category Category { get; set; } = null!;

    }
}
