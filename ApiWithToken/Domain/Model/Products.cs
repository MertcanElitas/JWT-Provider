using System;
using System.Collections.Generic;

namespace ApiWithToken.Domain.Model
{
    public partial class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal? Price { get; set; }
    }
}
