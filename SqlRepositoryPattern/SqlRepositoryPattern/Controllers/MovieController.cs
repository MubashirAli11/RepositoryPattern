using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.IBLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DataTransferObject;
using Models.QueryFilters;
using Models.ReponseModels;

namespace SqlRepositoryPattern.Controllers
{
    [Produces("application/json")]
    [Route("api/Movie")]
    public class MovieController : Controller
    {
        private IMovieBLL _movieBLL;
        public MovieController(IMovieBLL movieBLL)
        {
            this._movieBLL = movieBLL;
        }

        [HttpGet("{id}")]
        public async Task<DataTransfer<MovieDTO>> Get(int id)
        {
            DataTransfer<MovieDTO> response = new DataTransfer<MovieDTO>();
            try
            {
                response.Response = await this._movieBLL.Get(id);
            }
            catch (Exception exp)
            {
                response.IsSuccess = false;
                response.Message = exp.Message;
            }
            return response;
        }

        [HttpGet("GetFilteredData")]
        public async Task<DataTransfer<DataList<MovieDTO>>> GetFilteredData([FromQuery] MovieFilter movieFilter)
        {
            DataTransfer<DataList<MovieDTO>> response = new DataTransfer<DataList<MovieDTO>>();
            try
            {
                response.Response = await this._movieBLL.GetFilteredData(movieFilter);
            }
            catch (Exception exp)
            {
                response.IsSuccess = false;
                response.Message = exp.Message;
            }
            return response;
        }

        [HttpGet("GetAllMovies")]
        public async Task<DataTransfer<DataList<MovieDTO>>> GetAll([FromQuery] int page = 1, int pageSize = 20)
        {
            DataTransfer<DataList<MovieDTO>> response = new DataTransfer<DataList<MovieDTO>>();
            try
            {
                response.Response = await this._movieBLL.GetAll(page, pageSize);
            }
            catch (Exception exp)
            {
                response.IsSuccess = false;
                response.Message = exp.Message;
            }
            return response;
        }

        /// <summary>
        /// Insert new entry in movie table
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataTransfer<bool>> Post([FromBody] MovieDTO movie)
        {
            DataTransfer<bool> response = new DataTransfer<bool>();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = "Requested data is not in correct format";
                    return response;
                }
                response.Response = await this._movieBLL.Insert(movie);
            }
            catch (Exception exp)
            {
                response.IsSuccess = false;
                response.Message = exp.Message;
            }
            return response;
        }

        [HttpPut("{id}")]
        public async Task<DataTransfer<bool>> Put(int id, [FromBody] MovieDTO movie)
        {
            DataTransfer<bool> response = new DataTransfer<bool>();
            try
            {
                if (id <= 0 || !ModelState.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = "Requested data is not in correct format";
                    return response;
                }
                response.Response = await this._movieBLL.Put(id, movie);
            }
            catch (Exception exp)
            {
                response.IsSuccess = false;
                response.Message = exp.Message;
            }
            return response;
        }
    }
}