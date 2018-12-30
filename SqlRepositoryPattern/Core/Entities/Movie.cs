using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities
{
    public class Movie:BaseEntity<int>
    {
        [MaxLength(250)]
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double IMDBRating { get; set; }
        [MaxLength(50)]
        public string Budget { get; set; }
        [MaxLength(50)]
        public string BoxOfficeAmount { get; set; }
    }
}

