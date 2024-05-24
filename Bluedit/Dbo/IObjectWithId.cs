using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bluedit.Dbo
{
    public interface IObjectWithId
    {
        long Id { get; set; }
    }
}
