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
using TCRS_server.Users;

namespace TCRS_server.Controllers
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
