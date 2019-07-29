using MovieRentalMVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MovieRentalMVCApplication.ViewModels;

namespace MovieRentalMVCApplication.Controllers
{
    public class MovieController : Controller
    {
        ApplicationDbContext Moviecontext;
        public MovieController()
        {
            Moviecontext = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            Moviecontext.Dispose();
        }
        // GET: Movie
        public ActionResult Index()
        {
            var movies = Moviecontext.Movies.Include(m=>m.MovieGenre).ToList();
            return View(movies);
        }
        public ActionResult MovieDetails(int? id)
        {
            var movie = Moviecontext.Movies.Include(m => m.MovieGenre).SingleOrDefault(c => c.Id == id);
            return View(movie);
        }
        public ActionResult MovieForm()
        {
            var Mgenre = Moviecontext.MovieGenres.ToList();
            var viewModel1 = new MovieViewModel
            {
                MovieGenre = Mgenre
            };
            return View(viewModel1);
        }
        //public ActionResult MovieDetails(int? id)
        //{
        //    var movie=Moviecontext.Movies.Include(c => c.MovieGenre.GenreType).SingleOrDefault(c => c.Id == id);
        //    return View(movie);
        //}
        public ActionResult EditMovie(int? id)
        {
            var movie = Moviecontext.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieViewModel
            {
                Movie = movie,
                MovieGenre=Moviecontext.MovieGenres.ToList()
            };
            return View("MovieForm", viewModel);
        }
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
                Moviecontext.Movies.Add(movie);
            else
            {
                var movieInDb = Moviecontext.Movies.Single(c => c.Id == movie.Id);
                movieInDb.MovieName = movie.MovieName;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.No_OfStock = movie.No_OfStock;
            }
            Moviecontext.SaveChanges();
            return RedirectToAction("Index", "Movie");
        }
        //public ActionResult DeleteCustomer(int? id)
        //{
        //    var customer = _context.Customers.Find(id);
        //    _context.Customers.Remove(customer);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index", "Customer");
        //}


    }
}