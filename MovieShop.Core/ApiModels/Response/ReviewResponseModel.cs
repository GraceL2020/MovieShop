using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.ApiModels.Response
{
    public class ReviewResponseModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public decimal Rating { get; set; }

        public string ReviewText { get; set; }
        public List<ReviewMovieResponseModel> MovieReviews { get; set; }
    }

    public class ReviewMovieResponseModel
    {
        public int MovieId { get; set; }
        public Decimal Rating { get; set; }
        public int UserId { get; set; }
        public String Name { get; set; }
        public String ReviewText { get; set; }
    }
}
