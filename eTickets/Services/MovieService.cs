using AutoMapper;
using eTickets.BLL.Interfaces;
using eTickets.DAL.Context;
using eTickets.DAL.Entities;
using eTickets.PL.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.PL.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MvcETicketsAppDbContext _context;

        public MovieService(IUnitOfWork unitOfWork, MvcETicketsAppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task AddNewMovieAsync(MovieViewModel movieVM)
        {
            var newMovie = new Movie()
            {
                Name = movieVM.Name,
                Description = movieVM.Description,
                Price = movieVM.Price,
                ImageURL = movieVM.ImageURL,
                CinemaId = movieVM.CinemaId,
                StartDate = movieVM.StartDate,
                EndDate = movieVM.EndDate,
                MovieCategory = movieVM.MovieCategory,
                ProducerId = movieVM.ProducerId
            };
            await _unitOfWork.MovieRepository.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in movieVM.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<MovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new MovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(MovieViewModel movieVM)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == movieVM.Id);

            if (movie != null)
            {
                movie.Name = movieVM.Name;
                movie.Description = movieVM.Description;
                movie.Price = movieVM.Price;
                movie.ImageURL = movieVM.ImageURL;
                movie.CinemaId = movieVM.CinemaId;
                movie.StartDate = movieVM.StartDate;
                movie.EndDate = movieVM.EndDate;
                movie.MovieCategory = movieVM.MovieCategory;
                movie.ProducerId = movieVM.ProducerId;
                await _context.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == movieVM.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in movieVM.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = movieVM.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
