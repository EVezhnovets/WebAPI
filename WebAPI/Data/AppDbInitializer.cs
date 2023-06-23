using WebAPI.Data.Models;

namespace WebAPI.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context!.Books.Any())
                {
                    context.Books.AddRange(
                        new Book()
                        {
                            Title = "1st Book Title",
                            Description = "1st Book Description",
                            IsRead = true,
                            DateRead = DateTime.Now.AddDays(-10),
                            Rate = 4,
                            Genre = "Biography",
                            DateAdded = DateTime.Now
                        },

                        new Book()
                        {
                            Title = "2st Book Title",
                            Description = "2st Book Description",
                            IsRead = false,
                            Genre = "Biography",
                            DateAdded = DateTime.Now,
                            CoverUrl = "https..."
                        });

                    context.SaveChanges();
                }
            }
        }
    }
}