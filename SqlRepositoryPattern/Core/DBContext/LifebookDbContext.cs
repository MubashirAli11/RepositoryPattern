using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DBContext
{
    public class LifebookDbContext:DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public LifebookDbContext()
        {

        }

        public LifebookDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Movie>().HasData(
            new
            {
                Id = 1,
                Name = "Avengers: Infinity War",
                ReleaseDate = Convert.ToDateTime("23-04-18"),
                IMDBRating = 8.5,
                Budget = "400M",
                BoxOfficeAmount = "2B"
            },
            new
            {
                Id = 2,
                Name = "The Wolf of Wall Street",
                ReleaseDate = Convert.ToDateTime("17-01-14"),
                IMDBRating = 8.2,
                Budget = "50M",
                BoxOfficeAmount = "389M"
            },
             new
             {
                 Id = 2,
                 Name = "The Wolf of Wall Street",
                 ReleaseDate = Convert.ToDateTime("17-01-14"),
                 IMDBRating = 8.2,
                 Budget = "50M",
                 BoxOfficeAmount = "389M"
             },
            new
            {
                Id = 3,
                Name = "Identity",
                ReleaseDate = Convert.ToDateTime("25-04-03"),
                IMDBRating = 7.3,
                Budget = "20M",
                BoxOfficeAmount = "90M"
            }
            );

            base.OnModelCreating(builder);
        }
    }
}
