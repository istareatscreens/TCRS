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
    [Route("Citation Type")]
    public class CitationTypeController : Controller
    {
        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;

        public CitationTypeController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Manager)]
        public IEnumerable<Citation_Type> GetCitationType()
        {
            return _db.GetAllCitationType<Citation_Type>(_databaseContext.Server, new Citation_Type());
        }
    }
}
