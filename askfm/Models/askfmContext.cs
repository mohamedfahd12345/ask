using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace askfm.Models
{
    public partial class askfmContext : DbContext
    {
        public askfmContext()
        {
        }

        public askfmContext(DbContextOptions<askfmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Follower> Followers { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=askfm;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("answer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(350)
                    .HasColumnName("description");

                entity.Property(e => e.FromUserId)
                    .HasMaxLength(450)
                    .HasColumnName("from_user_id");

                entity.Property(e => e.Main).HasColumnName("main");

                entity.Property(e => e.QestionId).HasColumnName("qestion_id");

                entity.HasOne(d => d.Qestion)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QestionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_answer_question");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.CvLink).HasColumnName("cv_link");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FacebookLink).HasColumnName("facebook_link");

                entity.Property(e => e.LinkedInLink).HasColumnName("linked_in_link");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.Photo).HasColumnName("photo");

                entity.Property(e => e.TweeterLink).HasColumnName("tweeter_link");

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Follower>(entity =>
            {
                entity.ToTable("follower");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChildId)
                    .HasMaxLength(450)
                    .HasColumnName("child_id");

                entity.Property(e => e.FatherId)
                    .HasMaxLength(450)
                    .HasColumnName("father_id");

                entity.HasOne(d => d.Child)
                    .WithMany(p => p.FollowerChildren)
                    .HasForeignKey(d => d.ChildId)
                    .HasConstraintName("FK_follower_AspNetUsers1");

                entity.HasOne(d => d.Father)
                    .WithMany(p => p.FollowerFathers)
                    .HasForeignKey(d => d.FatherId)
                    .HasConstraintName("FK_follower_AspNetUsers");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("likes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.QestionId).HasColumnName("qestion_id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Qestion)
                    .WithMany(p => p.LikesNavigation)
                    .HasForeignKey(d => d.QestionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_likes_question");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_likes_AspNetUsers");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("question");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(350)
                    .HasColumnName("description");

                entity.Property(e => e.FromUserId)
                    .HasMaxLength(450)
                    .HasColumnName("from_user_id");

                entity.Property(e => e.Likes).HasColumnName("likes");

                entity.Property(e => e.Reply).HasColumnName("reply");

                entity.Property(e => e.ToUserId)
                    .HasMaxLength(450)
                    .HasColumnName("to_user_id");

                entity.Property(e => e.Unknown).HasColumnName("unknown");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
