using System;
using System.Collections.Generic;

namespace askfm.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            LikesNavigation = new HashSet<Like>();
        }

        public int Id { get; set; }
        public string? Description { get; set; }
        public string? FromUserId { get; set; }
        public int? Likes { get; set; }
        public string? ToUserId { get; set; }
        public DateTime? Date { get; set; }
        public bool? Unknown { get; set; }
        public bool? Reply { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Like> LikesNavigation { get; set; }
    }
}
