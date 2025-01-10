using eTickets.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.DAL.Entities
{
    public class Movie : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }
        public List<Actor_Movie> Actors_Movies { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        //Producer
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
    }

    public enum MovieCategory
    {
        Action ,
        Comedy,
        Drama,
        Documentary,
        Cartoon,
        Horror
    }
}
