using eTickets.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BLL.Interfaces
{
    public interface IUnitOfWork 
    {
        public IMovieRepository MovieRepository { get; set; }
        public ICinemaRepositores CinemaRepository { get; set; }
        public IActorRepository ActorRepository { get; set; }
        public IProducerRepository ProducerRepository { get; set; }
    }
}
