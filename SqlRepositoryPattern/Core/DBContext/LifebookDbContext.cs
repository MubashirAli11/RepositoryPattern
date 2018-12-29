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
            base.OnModelCreating(builder);
            mappingEntityRelations();
        }

        private void mappingEntityRelations()
        {
            mappingMovieRelations();
        }

        private void mappingMovieRelations()
        {
            //TODO: will do.
        }
    }
}
