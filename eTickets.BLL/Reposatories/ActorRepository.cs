using eTickets.BLL.Interfaces;
using eTickets.BLL.Repositories;
using eTickets.DAL.Context;
using eTickets.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BLL.Reposatories
{
    public class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
        private readonly MvcETicketsAppDbContext _context;

        public ActorRepository(MvcETicketsAppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
