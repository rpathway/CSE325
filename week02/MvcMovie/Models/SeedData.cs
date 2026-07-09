using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcMovieContext>>()))
        {
            if (context.Movie.Any())
            {
                return;
            }

            context.Movie.AddRange(
                new Movie
                {
                    Title = "The Matrix",
                    ReleaseDate = DateTime.Parse("1999-03-31"),
                    Genre = "Sci Fi",
                    Rating = "R",
                    Price = 9.99M
                },

                new Movie
                {
                    Title = "Interstellar",
                    ReleaseDate = DateTime.Parse("2014-11-07"),
                    Genre = "Sci Fi",
                    Rating = "PG-13",
                    Price = 12.99M
                },

                new Movie
                {
                    Title = "The Dark Knight",
                    ReleaseDate = DateTime.Parse("2008-07-18"),
                    Genre = "Action",
                    Rating = "PG-13",
                    Price = 10.99M
                }
            );

            context.SaveChanges();
        }
    }
}