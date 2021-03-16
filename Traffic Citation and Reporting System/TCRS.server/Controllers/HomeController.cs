using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TCRS.Database;
using TCRS.Database.Model;
using TCRS.Server.Tokens;
using TCRS.Server.Users;

namespace TCRS.Server.Controllers
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
        [Authorize(Roles = Roles.Manager)]
        public IEnumerable<Person> GetPeople()
        {
            return _db.GetAll<Person>(_databaseContext.Server, new Person());
        }
    }
}
