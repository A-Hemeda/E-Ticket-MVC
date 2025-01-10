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
    public class ProducerRepository : GenericRepository<Producer> , IProducerRepository
    {
        private readonly MvcETicketsAppDbContext _context;

        public ProducerRepository(MvcETicketsAppDbContext context) : base(context) 
        {
            _context = context;
        }
    }
}
