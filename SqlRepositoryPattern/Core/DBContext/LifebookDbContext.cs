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
                ReleaseDate = DateTime.UtcNow.AddMonths(-8),
                IMDBRating = 8.5,
                Budget = "400M",
                BoxOfficeAmount = "2B",
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            },
            new
            {
                Id = 2,
                Name = "The Wolf of Wall Street",
                ReleaseDate = DateTime.UtcNow.AddYears(-5),
                IMDBRating = 8.2,
                Budget = "50M",
                BoxOfficeAmount = "389M",
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            },
            new
            {
                Id = 3,
                Name = "Identity",
                ReleaseDate = DateTime.UtcNow.AddYears(-15),
                IMDBRating = 7.3,
                Budget = "20M",
                BoxOfficeAmount = "90M",
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            }
            );

            base.OnModelCreating(builder);
        }
    }
}
