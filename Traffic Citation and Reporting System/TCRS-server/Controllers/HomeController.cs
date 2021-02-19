using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS_db;
using TCRS_db.Model;
namespace TCRS_server.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class HomeController : Controller
    {
        private readonly Person person;
        private readonly IDataAccess _db;

        public HomeController(IDataAccess db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Person> GetPeople()
        {

            return _db.GetAll<Person>(Global.ConnectionString, new Person());


        }
    }
}
