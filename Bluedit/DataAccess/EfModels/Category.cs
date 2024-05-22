using System;
using System.Collections.Generic;

namespace Bluedit.DataAccess.EfModels;

public partial class Category
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Title { get; set; } = null!;

    public virtual ICollection<Thread> Threads { get; set; } = new List<Thread>();
}
