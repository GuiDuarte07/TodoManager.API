using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoManager.Models;

namespace TodoManager.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TodoItem>(entity =>
            {
                entity.ToTable("TodoItems");

                entity.HasKey(t => t.Id);

                entity.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(t => t.Description)
                    .HasMaxLength(500)
                    .IsRequired(false);

                entity.Property(t => t.DueDate)
                .IsRequired(false);

                entity.Property(t => t.Status)
                    .IsRequired()
                    .HasConversion<int>();

                entity.Property(t => t.CreatedAt)
                    .IsRequired();

                entity.Property(t => t.UpdatedAt)
                    .IsRequired();


                entity.HasOne(t => t.User) 
                      .WithMany(u => u.TodoItems) 
                      .HasForeignKey(t => t.UserId)
                      .IsRequired() 
                      .OnDelete(DeleteBehavior.Cascade); 

            });

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(256);
                entity.Property(e => e.UserName).HasMaxLength(256);
            });
        }
    }
}