using Core.Entities;
using Models.DataTransferObject;
using Models.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DALRepository.IRepositories
{
    public interface IMovieRepository:IBaseRepository<Movie, int>
    {
        Task<MovieDTO> GetById(int id);
        Task<DataList<MovieDTO>> GetAllMovies(int page, int pageSize);
        Task<DataList<MovieDTO>> GetFilteredData(MovieFilter movieFilter);
    }
}
