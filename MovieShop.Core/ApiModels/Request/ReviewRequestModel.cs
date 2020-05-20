using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.ApiModels.Request
{
    public class ReviewRequestModel
    {
        public int MovieId { get; set; }

        public int UserId { get; set; }

        public decimal Rating { get; set; }

        public string ReviewText { get; set; }
    }
}
