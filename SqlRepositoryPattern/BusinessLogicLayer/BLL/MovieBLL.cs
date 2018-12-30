using BusinessLogicLayer.IBLL;
using Core.Entities;
using DALRepository.UnitOfWork;
using Models.DataTransferObject;
using Models.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BusinessLogicLayer.BLL
{
    public class MovieBLL : IMovieBLL
    {
        private IUnitOfWork _unitOfWork;

        public MovieBLL(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<MovieDTO> Get(int id)
        {
            var movie = await this._unitOfWork.MoviesRepository.GetById(id);
            return movie;
        }

        public async Task<DataList<MovieDTO>> GetFilteredData(MovieFilter movieFilter)
        {
            var movies = await this._unitOfWork.MoviesRepository.GetFilteredData(movieFilter);
            return movies;
        }

        public async Task<DataList<MovieDTO>> GetAll(int page, int pageSize)
        {
            var movies = await this._unitOfWork.MoviesRepository.GetAllMovies(page, pageSize);
            return movies;
        }

        public async Task<bool> Insert(MovieDTO movie)
        {
            Movie entity = new Movie();
            DataCopier<MovieDTO, Movie>.Copy(movie, entity);
            this._unitOfWork.MoviesRepository.Insert(entity);
            await this._unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Put(int id, MovieDTO movie)
        {
            Movie entity = new Movie();
            DataCopier<MovieDTO, Movie>.Copy(movie, entity);
            entity.Id = id;
            this._unitOfWork.MoviesRepository.Update(entity);
            await this._unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
