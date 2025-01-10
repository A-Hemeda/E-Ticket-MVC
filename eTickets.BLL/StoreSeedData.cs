//using eTickets.DAL.Context;
//using eTickets.DAL.Entities;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace eTickets.BLL
//{
//    public class StoreSeedData
//    {
//        public static async Task SeedDataAsync(MvcETicketsAppDbContext context, ILoggerFactory loggerFactory)
//        {
//            try
//            {
//                if (context.Actors != null && !context.Actors.Any())
//                {
//                    var ActorData = File.ReadAllText("../eTickets.BLL/SeedData/actor.json");
//                    var Actors = JsonSerializer.Deserialize<List<Actor>>(ActorData);

//                    if (Actors != null)
//                    {
//                        foreach (var actor in Actors)
//                            await context.Actors.AddAsync(actor);
//                        await context.SaveChangesAsync();
//                    }
//                }

//                if (context.Movies != null && !context.Movies.Any())
//                {
//                    var MovieData = File.ReadAllText("../eTickets.BLL/SeedData/movie.json");
//                    var Movies = JsonSerializer.Deserialize<List<Movie>>(MovieData);

//                    if (Movies != null)
//                    {
//                        foreach (var movie in Movies)
//                            await context.Movies.AddAsync(movie);
//                        await context.SaveChangesAsync();
//                    }
//                }
//                if (context.Cinemas != null && !context.Cinemas.Any())
//                {
//                    var CinemaData = File.ReadAllText("../eTickets.BLL/SeedData/cinema.json");
//                    var Cinemas = JsonSerializer.Deserialize<List<Cinema>>(CinemaData);

//                    if (Cinemas != null)
//                    {
//                        foreach (var cinema in Cinemas)
//                            await context.Cinemas.AddAsync(cinema);
//                        await context.SaveChangesAsync();
//                    }
//                }
//                if (context.Producers != null && !context.Producers.Any())
//                {
//                    var ProducerData = File.ReadAllText("../eTickets.BLL/SeedData/producer.json");
//                    var Producers = JsonSerializer.Deserialize<List<Producer>>(ProducerData);

//                    if (Producers != null)
//                    {
//                        foreach (var producer in Producers)
//                            await context.Producers.AddAsync(producer);
//                        await context.SaveChangesAsync();
//                    }
//                }
//                if (context.Actors_Movies != null && !context.Actors_Movies.Any())
//                {
//                    var Actors_MoviesData = File.ReadAllText("../eTickets.BLL/SeedData/actor_movie.json");
//                    var Actors_Movies = JsonSerializer.Deserialize<List<Actor_Movie>>(Actors_MoviesData);

//                    if (Actors_Movies != null)
//                    {
//                        foreach (var actors_movies in Actors_Movies)
//                            await context.Actors_Movies.AddAsync(actors_movies);
//                        await context.SaveChangesAsync();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var logger = loggerFactory.CreateLogger<StoreSeedData>();
//                logger.LogError(ex.Message);
//            }
//        }
//    }
//}
