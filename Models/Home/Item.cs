﻿using ESS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESS.Models.Home
{
    public class Item
    {
        public Tbl_Product Product { get; set; }
        public int Quantity { get; set; }
    
    }
}