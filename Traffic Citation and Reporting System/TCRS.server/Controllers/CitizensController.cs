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
using TCRS.Shared.Objects.Citizens;

namespace TCRS.Server.Controllers
{
    [ApiController]
    [Route ("api/[controller]/SearchbyCitizen")]
    public class CitizensController : Controller
    {
        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;

        public CitizensController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CitizenCitation>> GetCitationByCitizen([FromQuery] String first_name)
        { 
            if (first_name == null || first_name.Length > 20)
            {
                return NotFound("No Citizen with the First Name found");
            }
            try
            {
                var citizens = new List<CitizenCitation>();
                foreach (var citizen in _db.GetCitationsByCitizen(first_name, _databaseContext.Server))
                {
                    citizens.Add(
                        new CitizenCitation
                        {
                            citizen_id = citizen.Driver_Record.citizen_id,
                            first_name = citizen.Driver_Record.Citizen.first_name,
                            middle_name = citizen.Driver_Record.Citizen.middle_name,
                            last_name = citizen.Driver_Record.Citizen.last_name,
                            citation_id = citizen.citation_id,
                            citation_number = citizen.citation_number,
                            name = citizen.Citation_Type.name,
                            fine = Double.Parse(citizen.Citation_Type.fine),
                            date_recieved = citizen.date_recieved,
                            vehicle_id = citizen.Vehicle_Record.vehicle_id,
                            training_eligable = citizen.Citation_Type.training_eligable
                        }
                        );

                };

                return citizens;
            }
            catch (Exception e)
            {
                return NotFound("Not found");
            }
        }
    }
}
