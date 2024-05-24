namespace Bluedit.Dbo
{
    public class User : IObjectWithId
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        public string Mail { get; set; } = null!;
    }
}
