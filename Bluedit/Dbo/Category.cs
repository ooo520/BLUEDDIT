namespace bluedit.Dbo
{
    public class Category : IObjectWithId
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Title { get; set; } = null!;

    }
}
