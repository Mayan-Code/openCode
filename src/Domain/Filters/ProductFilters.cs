﻿using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Filters
{
    public class ProductFilters : BaseEntityFilter
    {
        public ProductCategoryType? ProductCategoryType { get; set; }
        public string Barcode { get; set; }
        public bool OnlyFavorite { get; set; }

    }
}
