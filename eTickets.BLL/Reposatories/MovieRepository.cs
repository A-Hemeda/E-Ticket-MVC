using eTickets.BLL.Interfaces;
using eTickets.BLL.Repositories;
using eTickets.DAL.Context;
using eTickets.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BLL.Reposatories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly MvcETicketsAppDbContext _context;

        public MovieRepository(MvcETicketsAppDbContext context ) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> Search(string name)
            => _context.Movies.Where(e => e.Name.Trim().ToLower().Contains(name.Trim().ToLower())).Include(m => m.Cinema).OrderBy(n => n.Name);

        public async Task<IEnumerable<Movie>> GetAllWithCinemaNameAsync()
            => await _context.Movies.Include(m => m.Cinema).OrderBy(n => n.Name).ToListAsync();

    }
}
