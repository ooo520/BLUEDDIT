using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bluedit.Dbo
{
    public interface IObjectWithId
    {
        long Id { get; set; }
    }
}
