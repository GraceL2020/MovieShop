using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.ApiModels.Response
{
    public class FavoriteResponseModel
    {

        public int MovieId { get; set; }

        public int UserId { get; set; }

        public List<FavoriteMovieResponseModel> FavoriteMovies { get; set; }

        public class FavoriteMovieResponseModel
        {
            public int Id { get; set; }
            public String Title { get; set; }
            public String PosterUrl { get; set; }
        }
    }

    
}
