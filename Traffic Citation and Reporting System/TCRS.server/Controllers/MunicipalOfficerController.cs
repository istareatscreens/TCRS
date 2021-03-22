using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TCRS.Database;
using TCRS.Database.Model;
using TCRS.Server.Tokens;
using TCRS.Server.Users;
using TCRS.Shared.Objects.MunicipalOfficer;

namespace TCRS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/GetMunicipalOfficersByManager")]
    public class MunicipalOfficerController : Controller
    {
        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;

        public MunicipalOfficerController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }
        [HttpGet]
        public ActionResult<IEnumerable<MunicipalOfficerDept>> GetMunicipalOfficersByManager([FromQuery] int manager_id)
        {
            if (manager_id == 0)
            {
                return NotFound("No Manager with this ID found");
            }
            try
            {
                var municipals = new List<MunicipalOfficerDept>();
                foreach (var municipal in _db.GetMunicipalOfficersByManager(manager_id, _databaseContext.Server))
                {
                    municipals.Add(
                        new MunicipalOfficerDept
                        {
                            munic_id = municipal.munic_id,
                            manager_id = municipal.manager_id,
                            name = municipal.name,
                            person_id = municipal.Person.person_id,
                            first_name = municipal.Person.first_name,
                            last_name = municipal.Person.last_name,
                            email = municipal.Person.email
                        }
                        );

                };

                return municipals;

            }
            catch (Exception e)
            {
                return NotFound("Not found");
            }
        }
    }
}

