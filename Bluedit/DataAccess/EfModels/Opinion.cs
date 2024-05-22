using System;
using System.Collections.Generic;

namespace Bluedit.DataAccess.EfModels;

public partial class Opinion
{
    public long Id { get; set; }

    public long AnswerId { get; set; }

    public bool Like { get; set; }

    public long AuthorId { get; set; }

    public virtual User Author { get; set; } = null!;
}
