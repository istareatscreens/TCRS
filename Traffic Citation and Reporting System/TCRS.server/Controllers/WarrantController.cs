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
        public ActionResult RemoveWarrant(DeleteWarrantObject deleteWarrantObject)
        {
            try
            {
                _db.ChangeWarrantStatus(deleteWarrantObject.reference_no, false, _databaseContext.Server);
                return Ok("Successfully Removed");
            }
            catch
            {
                return BadRequest("Bad Request");
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<WarrantData>> GetWarrants([FromQuery] string license_id)
        {
            try
            {
                var citizen = _db.GetCitizenInfoByLicenseID(license_id, _databaseContext.Server);
                if (citizen == null || citizen.Count() == 0)
                {
                    return BadRequest("Invalid license id");
                }

                var warrants = _db.GetCitizenWarrants(citizen.ToList().FirstOrDefault().citizen_id, _databaseContext.Server);

                var warrantsList = warrants.ToList().Select(warrant => new WarrantData
                {
                    reference_no = warrant.Wanted.reference_no,
                    status = warrant.Wanted.active_status,
                    crime = warrant.Wanted.crime,
                    dangerous = warrant.Wanted.dangerous
                }).ToList();

                return warrantsList;
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
            catch (Exception e)
            {
                return BadRequest("Invalid request");
            }
        }

    }
}
