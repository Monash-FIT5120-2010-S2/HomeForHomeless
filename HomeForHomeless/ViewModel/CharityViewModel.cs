﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using HomeForHomeless.Models;

namespace HomeForHomeless.ViewModel
{
    public class CharityList
    {
        public IPagedList<VictoriaCharity> Charities { get; set; }
    }
}
