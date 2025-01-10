//using eTickets.BLL;
//using eTickets.DAL.Context;
//using Microsoft.EntityFrameworkCore;

//namespace eTickets.PL.ApplySeedData
//{
//    public static class ApplySeedingData
//    {
//        public static async Task ApplySeedDataAsync(WebApplication app)
//        {
//            using (var scope = app.Services.CreateScope())
//            {
//                var services = scope.ServiceProvider;
//                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

//                try
//                {
//                    var context = services.GetRequiredService<MvcETicketsAppDbContext>();
//                    await context.Database.MigrateAsync();
//                    await StoreSeedData.SeedDataAsync(context, loggerFactory);
//                }
//                catch (Exception ex)
//                {
//                    var logger = loggerFactory.CreateLogger<MvcETicketsAppDbContext>();
//                    logger.LogError(ex.Message);
//                }
//            }
//        }
//    }
//}
