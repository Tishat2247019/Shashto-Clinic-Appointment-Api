﻿using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class Repo
    {
        protected ClinicDbContext db;

        internal Repo()
        {
            db = new ClinicDbContext();
        }
    }
}
