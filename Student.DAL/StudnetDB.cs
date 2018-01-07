namespace Student.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StudnetDB : DbContext
    {
        public StudnetDB()
            : base("name=StudnetDB")
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(e => e.Subjects)
                .WithMany(e => e.Students)
                .Map(m => m.ToTable("SubjectInroll").MapLeftKey("StudentId").MapRightKey("SubjectId"));
        }
    }
}
