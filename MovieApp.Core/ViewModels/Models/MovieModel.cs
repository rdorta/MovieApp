using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Core.ViewModels.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Poster { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Overview { get; set; }
    }
}
