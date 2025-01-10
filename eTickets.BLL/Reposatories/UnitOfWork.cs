using eTickets.BLL.Interfaces;
using eTickets.DAL.Context;
using eTickets.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IMovieRepository movieRepository
                          , ICinemaRepositores cinemaRepository
                          , IActorRepository actorRepository
                          , IProducerRepository producerRepository)
        {
            MovieRepository = movieRepository;
            CinemaRepository = cinemaRepository;
            ActorRepository = actorRepository;
            ProducerRepository = producerRepository;
        }

        public IMovieRepository MovieRepository { get ; set; }
        public ICinemaRepositores CinemaRepository { get ; set; }
        public IActorRepository ActorRepository { get ; set; }
        public IProducerRepository ProducerRepository { get ; set; }


    }
}
