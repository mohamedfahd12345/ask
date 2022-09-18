using System;
using System.Collections.Generic;

namespace askfm.Models
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            AspNetUserTokens = new HashSet<AspNetUserToken>();
            FollowerChildren = new HashSet<Follower>();
            FollowerFathers = new HashSet<Follower>();
            Likes = new HashSet<Like>();
            Roles = new HashSet<AspNetRole>();
        }

        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string? CvLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? LinkedInLink { get; set; }
        public string? TweeterLink { get; set; }
        public string? Photo { get; set; }

        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual ICollection<Follower> FollowerChildren { get; set; }
        public virtual ICollection<Follower> FollowerFathers { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<AspNetRole> Roles { get; set; }
    }
}
