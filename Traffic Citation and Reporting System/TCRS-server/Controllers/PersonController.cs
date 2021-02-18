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
        private readonly TCRS_db.DataAccess db;

        [HttpPost]
        public IEnumerable<Person> Get()
        {
            return db.GetAll<Person>(Global.ConnectionString, new Person());
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
