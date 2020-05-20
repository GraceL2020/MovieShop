using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.ApiModels.Request
{
    public class PurchaseRequestModel
    {

        public int UserId { get; set; }

        public Guid PurchaseNumber { get; set; }

        public decimal? TotalPrice { get; set; }

        public DateTime PurchaseDateTime { get; set; }

        public int MovieId { get; set; }


    }
}
