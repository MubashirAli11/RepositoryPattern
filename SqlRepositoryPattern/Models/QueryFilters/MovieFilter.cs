using System;
using System.Collections.Generic;
using System.Text;

namespace Models.QueryFilters
{
    public class MovieFilter
    {
        public DateTime? FromReleaseDate { get; set; }
        public DateTime? ToReleaseDate { get; set; }
        public double IMDBRating { get; set; }
        public double Budget { get; set; }
        public double BoxOfficeAmount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public MovieFilter()
        {
            if (Page <= 0)
            {
                Page = 1;
            }
            if (PageSize <= 0)
            {
                PageSize = 20;
            }
        }
    }
}
