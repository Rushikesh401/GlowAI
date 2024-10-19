using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinData.Domain
{
    public class ProductRecommendation
    {
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public string WhatItContains { get; set; }
        public string WhyWeShouldDoIt { get; set; }
    }
}
