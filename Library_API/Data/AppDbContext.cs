using Library_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Book> book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData( new Book()
            {
                Id = 101,
                Title = "The Phantom Abyss",
                Author = "Alice Johansson",
                Genre = "Fantasy",
                IsAvalible = true,
                Description = "A journey through a mysterious world hidden beneath the earth's surface, filled with ancient magic and forgotten creatures.",
                ReleaseDate = new DateTime(12 / 9 / 2021)
            },
            new Book()
            {
                Id = 102,
                Title = "Whispers in the Dark",
                Author = "Liam Eriksson",
                Genre = "Horror",
                IsAvalible = false,
                Description = "A small town is haunted by strange voices that echo through the night, leading its residents to unravel a chilling secret.",
                ReleaseDate = new DateTime(14 / 2 / 2018)
            },
            new Book()
            {
                Id = 103,
                Title = "Galactic Frontiers",
                Author = "Sofia Lundgren",
                Genre = "Science Fiction",
                IsAvalible = true,
                Description = "In a distant future, humanity must explore uncharted galaxies to find new habitable planets, but they are not alone.",
                ReleaseDate = new DateTime(19 / 7 / 2023)
            },
            new Book()
            {
                Id = 104,
                Title = "Echoes of the Past",
                Author = "Oskar Nilsson",
                Genre = "Historical Fiction",
                IsAvalible = false,
                Description = "Set during World War II, a young woman uncovers a long-lost family secret that changes her life forever.",
                ReleaseDate = new DateTime(22 / 3 / 2020)
            },
            new Book()
            {
                Id = 105,
                Title = "The Quantum Enigma",
                Author = "Nina Bergström",
                Genre = "Thriller",
                IsAvalible = true,
                Description = "A brilliant scientist's groundbreaking discovery threatens to disrupt the fabric of reality, and now everyone is after it.",
                ReleaseDate = new DateTime(11 / 11 / 2017)
            }

           );
        }
    }
}
