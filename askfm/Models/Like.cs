using System;
using System.Collections.Generic;

namespace askfm.Models
{
    public partial class Like
    {
        public int Id { get; set; }
        public int? QestionId { get; set; }
        public string? UserId { get; set; }

        public virtual Question? Qestion { get; set; }
        public virtual AspNetUser? User { get; set; }
    }
}
