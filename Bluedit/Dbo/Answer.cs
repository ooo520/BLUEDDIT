using Bluedit.DataAccess.EfModels;
using System.ComponentModel.DataAnnotations;

namespace Bluedit.Dbo
{
    public class Answer : IObjectWithId
    {
        public long Id { get; set; }

        public string Content { get; set; } = null!;

        public long UserId { get; set; }

        public long ThreadId { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
