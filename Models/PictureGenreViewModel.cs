using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVCGallery.Models
{
    public class PictureGenreViewModel
    {
        public List<Picture> Pictures;
        public SelectList Genres;
        public string PictureGenre { get; set; }
        public string SearchString { get; set; }
    }
}