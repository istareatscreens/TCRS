using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using TCRS.Database;
using TCRS.Database.Model;
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
        public ActionResult PostPayment([FromBody] string reference_number)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public ActionResult<IEnumerable<WarrantData>> GetWarrants([FromBody] string license_id)
        {
            try
            {
                var citizen = _db.GetCitizenInfoByLicenseID(license_id, _databaseContext.Server);
                if (citizen == null || citizen.Count() == 0)
                {
                    return BadRequest("Invalid Citizen");
                }

                var warrants = _db.GetCitizenWarrants(citizen.ToList().FirstOrDefault().citizen_id, _databaseContext.Server);

                return Ok(warrants.ToList().Select(warrant => new WarrantData
                {
                    reference_no = warrant.Wanted.reference_no,
                    status = warrant.Wanted.active_status,
                    crime = warrant.Wanted.crime
                }));

            }
            catch
            {
                return BadRequest("Unknown Error");
            }
        }

        //TEST IMPLEMENTATION (Need to integrate payment API)
        [HttpPost("PostWarrant")]
        public ActionResult PostWarrant(CreateWarrantObject CreateWarrantObject)
        {
            try
            {
                var citizen = _db.GetCitizenInfoByLicenseID(CreateWarrantObject.license_id, _databaseContext.Server);
                if (citizen == null || citizen.Count() == 0)
                {
                    return BadRequest("Invalid Citizen");
                }

                _db.CreateCitizenWanted(new Wanted
                {
                    crime = CreateWarrantObject.crime,
                    dangerous = CreateWarrantObject.dangerous,
                    reference_no = CreateWarrantObject.reference_no
                }, citizen.ToList().FirstOrDefault().citizen_id, _databaseContext.Server);

                return Ok("Successfully posted");
            }
            catch
            {
                return BadRequest("Invalid request");
            }
        }

    }
}
