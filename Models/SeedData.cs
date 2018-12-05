using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MVCGallery.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MVCGalleryContext(
                serviceProvider.GetRequiredService<DbContextOptions<MVCGalleryContext>>()))
            {
                // Look for any movies.
                if (context.Picture.Any())
                {
                    return;   // DB has been seeded
                }

                context.Picture.AddRange(
                     new Picture
                     {
                         Title = "When Harry Met Sally",
                         ReleaseDate = DateTime.Parse("1989-1-11"),
                         Genre = "Romantic Comedy",
                         Rating = "R",
                         Price = 7.99M
                     },

                     new Picture
                     {
                         Title = "Ghostbusters ",
                         ReleaseDate = DateTime.Parse("1984-3-13"),
                         Genre = "Comedy",
                         Price = 8.99M
                     },

                     new Picture
                     {
                         Title = "Ghostbusters 2",
                         ReleaseDate = DateTime.Parse("1986-2-23"),
                         Genre = "Comedy",
                         Price = 9.99M
                     },

                   new Picture
                   {
                       Title = "Rio Bravo",
                       ReleaseDate = DateTime.Parse("1959-4-15"),
                       Genre = "Western",
                       Price = 3.99M
                   }
                );
                context.SaveChanges();
            }
        }
    }
}