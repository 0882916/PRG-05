using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVCGallery.Models
{
    public class MVCGalleryContext : DbContext
    {
        public MVCGalleryContext (DbContextOptions<MVCGalleryContext> options)
            : base(options)
        {
        }

        public DbSet<MVCGallery.Models.Picture> Picture { get; set; }
    }
}
