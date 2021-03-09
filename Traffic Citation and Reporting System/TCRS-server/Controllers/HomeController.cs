using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS_db;
using TCRS_db.Model;
using TCRS_server.Tokens;

namespace TCRS_server.Controllers
{
    [ApiController]
    [Route("test")]
    public class HomeController : Controller
    {
        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;

        public HomeController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Person> GetPeople()
        {
            return _db.GetAll<Person>(_databaseContext.Server, new Person());
        }
    }
}
