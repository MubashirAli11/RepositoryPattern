using Core.DBContext;
using DALRepository.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DALRepository.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly LifebookDbContext _context;
        private IMovieRepository _moviesRepository;

        public UnitOfWork(LifebookDbContext context,
                          IMovieRepository moviesRepository)
        {
            this._context = context;
            this._moviesRepository = moviesRepository;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await this._context.SaveChangesAsync();
        }
        public IMovieRepository MoviesRepository
        {
            get
            {
                return this._moviesRepository;
            }
        }
    }
}
