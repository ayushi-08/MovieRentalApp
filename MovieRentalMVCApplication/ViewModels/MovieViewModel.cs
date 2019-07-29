using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieRentalMVCApplication.Models;

namespace MovieRentalMVCApplication.ViewModels
{
    public class MovieViewModel
    {
        public IEnumerable<MovieGenre> MovieGenre { get; set; }
        public Movie Movie { get; set; }
    }
}