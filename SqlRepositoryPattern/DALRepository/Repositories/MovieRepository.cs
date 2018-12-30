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
        protected MovieRepository(LifebookDbContext context) : base(context)
        {
        }

        public async Task<List<Movie>> GetFilteredData(MovieFilter movieFilter)
        {
            var expression = BuildQueryExpression(movieFilter);
            return await base.GetFilteredData(expression);
        }

        public async Task<DataList<Movie>> GetFilteredDataWithPaging(MovieFilter movieFilter,
                                       int page, int pageSize)
        {
            var expression = BuildQueryExpression(movieFilter);
            return await base.GetFilteredDataWithPaging(expression, page, pageSize);
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
