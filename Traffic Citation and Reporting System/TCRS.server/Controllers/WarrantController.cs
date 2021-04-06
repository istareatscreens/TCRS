using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using TCRS.Database;
using TCRS.Server.Tokens;
using TCRS.Shared.Objects.Warrant;

namespace TCRS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarrantController : Controller
    {

        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;
        public WarrantController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }

        [HttpPost("RemoveWarrant")]
        public ActionResult PostPayment()
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public ActionResult<IEnumerable<WarrantData>> GetWarrants([FromBody] string license_id)
        {
            throw new NotImplementedException();
        }

        //TEST IMPLEMENTATION (Need to integrate payment API)
        [HttpPost("PostWarrant")]
        public ActionResult PostWarrant(WarrantData WarrantData)
        {
            try
            {
                /*
                var citizen = _db.GetCitizenInfoByLicenseID(WarrantData.);
                if (citizen == null)
                {
                    return BadRequest("Invalid Citizen");
                }
                
                _db.CreateCitizenWanted(new Wanted
                });
                */
                throw new NotImplementedException();

            }
            catch
            {
                return BadRequest("Invalid request");
            }
            throw new NotImplementedException();
        }

    }
}
