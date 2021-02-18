using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS_db.Model;

namespace TCRS_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        [HttpPost]
        public IEnumerable<Person> Post()
        {


        }


        /*
        private readonly 
        public IActionResult Index()
        {
            return View();
        }
        */
    }
}
