using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatWorks.Controllers
{
    public class accountController : Controller
    {
        [AllowAnonymous]
        public IActionResult login()
        {

            return View();
        }
    }
}
