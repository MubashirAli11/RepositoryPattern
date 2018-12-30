using Models.DataTransferObject;
using Models.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IBLL
{
    public interface IMovieBLL
    {
        Task<MovieDTO> Get(int id);
        Task<DataList<MovieDTO>> GetAll(int page, int pageSize);
        Task<DataList<MovieDTO>> GetFilteredData(MovieFilter movieFilter);
        Task<bool> Insert(MovieDTO movie);
        Task<bool> Put(int id, MovieDTO movie);
    }
}
