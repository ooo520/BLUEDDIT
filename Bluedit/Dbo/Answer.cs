using bluedit.DataAccess.EfModels;
using System.ComponentModel.DataAnnotations;

namespace bluedit.Dbo
{
    public class Answer : IObjectWithId
    {
        public long Id { get; set; }

        public string Content { get; set; } = null!;

        public long UserId { get; set; }

        public long ThreadId { get; set; }

        public DateTime CreationDate { get; set; }
		public Thread Thread { get; set; } = null!;
		public User User { get; set; } = null!;
        public long Likes {  get; set; } = 0!;
    }
}
