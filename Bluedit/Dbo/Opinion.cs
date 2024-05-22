﻿namespace Bluedit.Dbo
{
    public class Opinion : IObjectWithId
    {
        public long Id { get; set; }

        public long AnswerId { get; set; }

        public bool Like { get; set; }

        public long AuthorId { get; set; }
    }
}
