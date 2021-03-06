using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using TCRS.Database;
using TCRS.Database.Model;
using TCRS.Server.Tokens;
using TCRS.Shared.Objects.Auth;
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

        [HttpPut("RemoveWarrant")]
        [Authorize(Roles = Roles.HighwayPatrolOfficer + "," + Roles.Manager)]
        public ActionResult RemoveWarrant(DeleteWarrantObject deleteWarrantObject)
        {
            try
            {
                if (deleteWarrantObject.reference_no.Length > 36)
                {
                    return Ok(new { message = "Invalid reference_no" });
                }

                _db.ChangeWarrantStatus(deleteWarrantObject.reference_no, false, _databaseContext.Server);
                return Ok(new { message = "Successfully Removed" });
            }
            catch
            {
                return BadRequest(new { message = "Bad Request" });
            }
        }
        [HttpGet]
        [Authorize(Roles = Roles.HighwayPatrolOfficer + "," + Roles.Manager)]
        public ActionResult<IEnumerable<WarrantData>> GetWarrants([FromQuery] string license_id, [FromQuery] string plate_number)
        {
            try
            {
                if (license_id != null)
                {
                    var citizen = _db.GetCitizenInfoByLicenseID(license_id, _databaseContext.Server);
                    if (citizen == null || citizen.Count() == 0)
                    {
                        return BadRequest(new { message = "Invalid license id" });
                    }

                    var warrants = _db.GetCitizenWarrants(citizen.ToList().FirstOrDefault().citizen_id, _databaseContext.Server);

                    if (warrants == null && warrants.Count() == 0)
                    {
                        var emptyWarrantList = new List<WarrantData>();
                        emptyWarrantList.Add(new WarrantData());
                        return emptyWarrantList;
                    }

                    var warrantsList = warrants.ToList().Select(warrant => (warrant.Wanted != null) ? new WarrantData
                    {
                        reference_no = warrant.Wanted.reference_no,
                        status = warrant.Wanted.active_status,
                        crime = warrant.Wanted.crime,
                        dangerous = warrant.Wanted.dangerous
                    } : null);

                    return Ok(warrantsList);
                }
                else if (plate_number != null)
                {
                    var license = _db.GetVehicleInfoByLicensePlate(plate_number, _databaseContext.Server);
                    if (license == null || license.Count() == 0)
                    {
                        return BadRequest(new { message = "Invalid license id" });
                    }

                    var warrants = _db.GetVehicleWarrants(license.ToList().FirstOrDefault().vehicle_id, _databaseContext.Server);

                    if (warrants == null && warrants.Count() == 0)
                    {
                        var emptyWarrantList = new List<WarrantData>();
                        emptyWarrantList.Add(new WarrantData());
                        return emptyWarrantList;
                    }

                    var warrantsList = warrants.ToList().Select(warrant => (warrant.Wanted != null) ? new WarrantData
                    {
                        reference_no = warrant.Wanted.reference_no,
                        status = warrant.Wanted.active_status,
                        crime = warrant.Wanted.crime,
                        dangerous = warrant.Wanted.dangerous
                    } : null);

                    return Ok(warrantsList);


                }
                else
                {
                    return BadRequest(new { message = "Invalid input" });
                }
            }
            catch
            {
                return BadRequest(new { message = "No Warrants Exist" });
            }
        }

        //TEST IMPLEMENTATION (Need to integrate payment API)
        [HttpPost("PostWarrant")]
        [Authorize(Roles = Roles.HighwayPatrolOfficer + "," + Roles.Manager)]
        public ActionResult PostWarrant(CreateWarrantObject CreateWarrantObject)
        {
            try
            {
                if (CreateWarrantObject.license_id != null && CreateWarrantObject.license_id != "")
                {

                    var citizen = _db.GetCitizenInfoByLicenseID(CreateWarrantObject.license_id, _databaseContext.Server);
                    if (citizen == null || citizen.Count() == 0)
                    {
                        return BadRequest(new { message = "Invalid Citizen" });
                    }

                    _db.CreateCitizenWanted(new Wanted
                    {
                        crime = CreateWarrantObject.crime,
                        dangerous = CreateWarrantObject.dangerous,
                        reference_no = CreateWarrantObject.reference_no,
                        active_status = true
                    }, citizen.ToList().FirstOrDefault().citizen_id, _databaseContext.Server);

                    return Ok(new { message = "Successfully posted" });

                }
                else if (CreateWarrantObject.plate_number != null && CreateWarrantObject.plate_number != "")
                {
                    var vehicle = _db.GetVehicleInfoByLicensePlate(CreateWarrantObject.plate_number, _databaseContext.Server);
                    if (vehicle == null || vehicle.Count() == 0)
                    {
                        return BadRequest(new { message = "Invalid Citizen" });
                    }

                    _db.CreateVehicleWanted(new Wanted
                    {
                        crime = CreateWarrantObject.crime,
                        dangerous = CreateWarrantObject.dangerous,
                        reference_no = CreateWarrantObject.reference_no,
                        active_status = true
                    }, vehicle.ToList().FirstOrDefault().vehicle_id, _databaseContext.Server);

                    return Ok(new { message = "Successfully posted" });
                }
                else
                {
                    return BadRequest(new { message = "Invalid request" });
                }
            }
            catch
            {
                return BadRequest(new { message = "Invalid request or Duplicate Entry" });
            }
        }

    }
}
