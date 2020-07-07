namespace SMS.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SMSModel : DbContext
    {
        public SMSModel()
            : base("name=SMSModel")
        {
        }

        public virtual DbSet<course> course { get; set; }
        public virtual DbSet<department> department { get; set; }
        public virtual DbSet<discussion> discussion { get; set; }
        public virtual DbSet<student> student { get; set; }
        public virtual DbSet<student_class> student_class { get; set; }
        public virtual DbSet<teacher> teacher { get; set; }
        public virtual DbSet<user> user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<course>()
                .Property(e => e.course_name)
                .IsUnicode(false);

            modelBuilder.Entity<course>()
                .Property(e => e.broadCast)
                .IsUnicode(false);

            modelBuilder.Entity<course>()
                .HasMany(e => e.discussion)
                .WithRequired(e => e.course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<course>()
                .HasMany(e => e.student_class)
                .WithRequired(e => e.course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<department>()
                .Property(e => e.dep_name)
                .IsUnicode(false);

            modelBuilder.Entity<discussion>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<discussion>()
                .HasMany(e => e.discussion1)
                .WithOptional(e => e.discussion2)
                .HasForeignKey(e => e.dis_dis_id);

            modelBuilder.Entity<student>()
                .HasMany(e => e.student_class)
                .WithRequired(e => e.student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<teacher>()
                .Property(e => e.job_title)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.tel)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.discussion)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasOptional(e => e.student)
                .WithRequired(e => e.user);

            modelBuilder.Entity<user>()
                .HasOptional(e => e.teacher)
                .WithRequired(e => e.user);
        }
    }
}
