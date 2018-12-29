﻿using Core.DBContext;
using Core.Entities;
using DALRepository.IRepositories;
using Microsoft.EntityFrameworkCore;
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
        /// <summary>
        /// replace with generic method 
        /// </summary>
        /// <param name="movieFilter"></param>
        /// <returns></returns>
        public async Task<List<Movie>> GetWithFilters(MovieFilter movieFilter)
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
            if (movieFilter.Budget > 0)
            {
                if (exp1 == null)
                {
                    exp1 = x => x.Budget > movieFilter.Budget;
                }
                else
                {
                    exp2 = x => x.Budget > movieFilter.Budget;
                    exp1 = QueryExpression.CreateAndExpression(exp1, exp2);
                }
            }
            if (movieFilter.BoxOfficeAmount > 0)
            {
                if (exp1 == null)
                {
                    exp1 = x => x.BoxOfficeAmount > movieFilter.BoxOfficeAmount;
                }
                else
                {
                    exp2 = x => x.BoxOfficeAmount > movieFilter.BoxOfficeAmount;
                    exp1 = QueryExpression.CreateAndExpression(exp1, exp2);
                }
            }
            if (exp1 != null)
            {
                return await _DefaultQuery.Where(exp1).ToListAsync();
            }
            else
            {
                return await _DefaultQuery.ToListAsync();
            }
        }
    }
}