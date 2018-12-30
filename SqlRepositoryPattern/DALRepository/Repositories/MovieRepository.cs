using Core.DBContext;
using Core.Entities;
using DALRepository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models.DataTransferObject;
using Models.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.QueryUtility;

namespace DALRepository.Repositories
{
    public class MovieRepository : BaseRepository<Movie, int>, IMovieRepository
    {
        public MovieRepository(LifebookDbContext context) : base(context)
        {
        }

        public async Task<MovieDTO> GetById(int id)
        {
            var query = base.Get(id);
            var data = await (from movie in query
                              select new MovieDTO
                              {
                                  Name = movie.Name,
                                  IMDBRating = movie.IMDBRating,
                                  Budget = movie.Budget,
                                  BoxOfficeAmount = movie.BoxOfficeAmount,
                                  ReleaseDate = movie.ReleaseDate,
                                  CreatedOn = movie.CreatedOn,
                                  LastModifiedOn = movie.LastModifiedOn,
                                  CreatedBy = movie.CreatedBy,
                                  IsActive = movie.IsActive,
                                  IsDeleted = movie.IsDeleted,
                                  LastModifiedBy = movie.LastModifiedBy
                              }).FirstOrDefaultAsync();
            return data;
        }

        public async Task<DataList<MovieDTO>> GetAllMovies(int page, int pageSize)
        {
            var query = base.GetAll();
            DataList<MovieDTO> dataList = new DataList<MovieDTO>();
            dataList.TotalCount = query.Count();
            int skip = (page - 1) * pageSize;
            int take = skip + pageSize;
            dataList.Data = await (from movie in query
                              select new MovieDTO
                              {
                                  Name = movie.Name,
                                  IMDBRating = movie.IMDBRating,
                                  Budget = movie.Budget,
                                  BoxOfficeAmount = movie.BoxOfficeAmount,
                                  ReleaseDate = movie.ReleaseDate,
                                  CreatedOn = movie.CreatedOn,
                                  LastModifiedOn = movie.LastModifiedOn,
                                  CreatedBy = movie.CreatedBy,
                                  IsActive = movie.IsActive,
                                  IsDeleted = movie.IsDeleted,
                                  LastModifiedBy = movie.LastModifiedBy
                              }).OrderByDescending(x=>x.IsDeleted)
                              .Skip(skip)
                              .Take(take)
                              .ToListAsync();
            return dataList;
        }

        public async Task<DataList<MovieDTO>> GetFilteredData(MovieFilter movieFilter)
        {
            var expression = BuildQueryExpression(movieFilter);
            if (expression != null)
            {
                var query = base.GetFilteredData(expression);
                DataList<MovieDTO> dataList = new DataList<MovieDTO>();
                dataList.TotalCount = query.Count();
                int skip = (movieFilter.Page - 1) * movieFilter.PageSize;
                int take = skip + movieFilter.PageSize;
                dataList.Data = await (from movie in query
                                       select new MovieDTO
                                       {
                                           Name = movie.Name,
                                           IMDBRating = movie.IMDBRating,
                                           Budget = movie.Budget,
                                           BoxOfficeAmount = movie.BoxOfficeAmount,
                                           ReleaseDate = movie.ReleaseDate,
                                           CreatedOn = movie.CreatedOn,
                                           LastModifiedOn = movie.LastModifiedOn,
                                           CreatedBy = movie.CreatedBy,
                                           IsActive = movie.IsActive,
                                           IsDeleted = movie.IsDeleted,
                                           LastModifiedBy = movie.LastModifiedBy
                                       }).OrderByDescending(x => x.IsDeleted)
                               .Skip(skip)
                               .Take(take)
                               .ToListAsync();
                return dataList;
            }
            else
            {
                return await GetAllMovies(movieFilter.Page, movieFilter.PageSize);
            }
        }
        /// <summary>
        /// replace with generic method 
        /// </summary>
        /// <param name="movieFilter"></param>
        /// <returns></returns>
        private Expression<Func<Movie, bool>> BuildQueryExpression(MovieFilter movieFilter)
        {
            Expression<Func<Movie, bool>> exp1 = null;
            Expression<Func<Movie, bool>> exp2 = null;
            if (movieFilter.FromReleaseDate != null)
            {
                exp1 = x => x.ReleaseDate >= movieFilter.FromReleaseDate.Value;
            }
            if (movieFilter.ToReleaseDate != null)
            {
                if (exp1 == null)
                {
                    exp1 = x => x.ReleaseDate <= movieFilter.ToReleaseDate.Value;
                }
                else
                {
                    exp2 = x => x.ReleaseDate <= movieFilter.ToReleaseDate.Value;
                    exp1 = QueryExpression.CreateAndExpression(exp1, exp2);
                }             
            }
            if (movieFilter.IMDBRating > 0)
            {
                if (exp1 == null)
                {
                    exp1 = x => x.IMDBRating == movieFilter.IMDBRating;
                }
                else
                {
                    exp2 = x => x.IMDBRating == movieFilter.IMDBRating;
                    exp1 = QueryExpression.CreateAndExpression(exp1, exp2);
                }
            }
            return exp1;
        }

    }
}
