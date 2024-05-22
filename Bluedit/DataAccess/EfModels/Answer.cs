using System;
using System.Collections.Generic;

namespace Bluedit.DataAccess.EfModels;

public partial class Answer
{
    public long Id { get; set; }

    public string Content { get; set; } = null!;

    public long UserId { get; set; }

    public long ThreadId { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual ICollection<Opinion> Opinions { get; set; } = new List<Opinion>();

    public virtual Thread Thread { get; set; } = null!;

    public virtual ICollection<Thread> Threads { get; set; } = new List<Thread>();

    public virtual User User { get; set; } = null!;
}
