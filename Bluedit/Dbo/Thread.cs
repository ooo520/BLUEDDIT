namespace Bluedit.Dbo
{
    public class Thread
    {
        public long Id { get; set; }

        public string Title { get; set; } = null!;

        public long CategoryId { get; set; }

        public long RootAnswerId { get; set; }

    }
}
