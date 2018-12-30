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
    public interface IMovieRepository:IBaseRepository<Movie,int>
    {
        Task<List<Movie>> GetFilteredData(MovieFilter movieFilter);
        Task<DataList<Movie>> GetFilteredDataWithPaging(MovieFilter movieFilter,
                                       int page, int pageSize);
    }
}
