﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer
{
    public class PagedResult<T> : PagedResult
    {
        public IEnumerable<T> PageResults { get; set; }
    }
}
