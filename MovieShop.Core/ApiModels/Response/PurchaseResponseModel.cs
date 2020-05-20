using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.ApiModels.Response
{
    public class PurchaseResponseModel
    {
        public int UserId { get; set; }
        public List<PurchasedMovieResponseModel> PurchasedMovies { get; set; }

        public class PurchasedMovieResponseModel
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string PosterUrl { get; set; }

            public DateTime PurchaseDateTime { get; set; }
        }
    }

   
}
