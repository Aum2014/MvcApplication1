using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MvcApplication2.Models
{
    public class Movies
    {

        public int ID { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }

    public class MoviesDBContext : DbContext
    {
        public DbSet<Movies> Movies { get; set; }
    }
}