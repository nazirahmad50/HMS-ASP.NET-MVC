﻿using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<AccomadationType> AccomadationTypes { get; set; }
    }
}