using BookFinder.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BookFinder.Context
{
    public class BookFinderContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "BookFinderDb");
        }

        public BookFinderContext(DbContextOptions<BookFinderContext> options) : base(options) { }

        #region DbSets

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAuthorAssociation> BookAuthorAssociations { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                   .HasMany(t => t.Authors)
                   .WithMany(t => t.Books)
                   .UsingEntity<BookAuthorAssociation>();

            modelBuilder.Entity<Author>()
                   .HasMany(t => t.Books)
                   .WithMany(t => t.Authors)
                   .UsingEntity<BookAuthorAssociation>();
        }
    }
}
