using System;
using System.Collections.Generic;

namespace bluedit.DataAccess.EfModels;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public string Mail { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<Opinion> Opinions { get; set; } = new List<Opinion>();
}
