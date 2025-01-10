using AutoMapper;
using eTickets.BLL.Interfaces;
using eTickets.DAL.Entities;
using eTickets.PL.Models;
using eTickets.PL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTickets.PL.Controllers
{
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieService _movieServices;
        private readonly IMapper _mapper;

        public MovieController(IUnitOfWork unitOfWork, IMovieService movieServices, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _movieServices = movieServices;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue = "")
        {
            IEnumerable<Movie> movies;
            if (string.IsNullOrEmpty(SearchValue))
            {
                movies = await _unitOfWork.MovieRepository.GetAllWithCinemaNameAsync();
            }
            else
            {
                movies = await _unitOfWork.MovieRepository.Search(SearchValue);
            }
            return View(movies);
        }

        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _movieServices.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel movieVM)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _movieServices.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movieVM);
            }

            await _movieServices.AddNewMovieAsync(movieVM);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _movieServices.GetMovieByIdAsync(id);
            return View(movieDetail);
        }

        public async Task<IActionResult> Update(int id)
        {
            var movieDetails = await _movieServices.GetMovieByIdAsync(id);
            if (movieDetails == null) return View("NotFound");

            var response = new MovieViewModel()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
            };

            var movieDropdownsData = await _movieServices.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, MovieViewModel movieVM)
        {
            if (id != movieVM.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _movieServices.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movieVM);
            }

            await _movieServices.UpdateMovieAsync(movieVM);
            return RedirectToAction(nameof(Index));
        }
    }
}
