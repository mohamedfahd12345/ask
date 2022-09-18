using System;
using System.Collections.Generic;

namespace askfm.Models
{
    public partial class Answer
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool? Main { get; set; }
        public DateTime? Date { get; set; }
        public string? FromUserId { get; set; }
        public int? QestionId { get; set; }

        public virtual Question? Qestion { get; set; }
    }
}
