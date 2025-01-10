using AutoMapper;
using eTickets.DAL.Entities;
using eTickets.PL.Models;

namespace eTickets.PL.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ActorViewModel, Actor>().ReverseMap();
            CreateMap<CinemaViewModel, Cinema>().ReverseMap();
            CreateMap<ProducerViewModel, Producer>().ReverseMap();
            CreateMap<MovieViewModel, Movie>().ReverseMap();
        }
    }
}
