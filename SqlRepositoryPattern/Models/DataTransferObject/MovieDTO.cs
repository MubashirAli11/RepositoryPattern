using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DataTransferObject
{
    public class MovieDTO
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double IMDBRating { get; set; }
        public string Budget { get; set; }
        public string BoxOfficeAmount { get; set; }
        /// <summary>
        /// Derived column. It will show you whether
        /// this movie is available in
        /// your country or not?
        /// </summary>
        public string Available { get; set; }
    }
}
