using eTickets.DAL.Entities;
using eTickets.PL.Models;

namespace eTickets.PL.Services
{
    public interface IMovieService
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<MovieDropdownsVM> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(MovieViewModel movieVM);
        Task UpdateMovieAsync(MovieViewModel movieVM);
    }
}
