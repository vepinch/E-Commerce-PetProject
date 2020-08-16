﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceStore.Models
{
    public class Product
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }
    }
}
