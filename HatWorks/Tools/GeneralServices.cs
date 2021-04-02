using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatWorks.Tools
{
    public class Context
    {
        public static HttpContext _current() => new HttpContextAccessor().HttpContext;
    }
}
