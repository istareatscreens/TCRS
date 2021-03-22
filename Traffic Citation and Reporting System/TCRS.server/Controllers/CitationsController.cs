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
using TCRS.Shared.Objects.Citations;

namespace TCRS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitationsController : Controller
    {
        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;

        public CitationsController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }
        [HttpGet]
        public  ActionResult<IEnumerable<CitizenVehicleCitation>> GetCitationsByCitizen([FromQuery]String plate_number)
        {
        //Return type is wrapped in action result to allow NotFond to be returned

            //I pull licenseplate url query parameter here to be passed to database query
            if(plate_number == null || plate_number.Length > 8)
            {
                return NotFound("No License Plate Specified");
            }
            try
            {
                var citations = new List<CitizenVehicleCitation>();
                foreach (var citation in _db.GetCitationsByLicensePlate(plate_number, _databaseContext.Server))
                {
                    citations.Add(
                        new CitizenVehicleCitation
                        {
                            citation_id = citation.citation_id,
                            citation_number = citation.citation_number,
                            name = citation.Citation_Type.name,
                            fine = Double.Parse(citation.Citation_Type.fine),
                            date_recieved = citation.date_recieved,
                            vehicle_id = citation.Vehicle_Record.vehicle_id,
                            training_eligable = citation.Citation_Type.training_eligable,
                            plate_number = plate_number
                        }
                        );

                };

                return citations;

            }
            catch(Exception e)
            {
                return NotFound("Not found");
            }
        }
        [HttpGet("All")]
        public IEnumerable<Citation_Type> GetCitationType()
        {     
            return _db.GetAllCitationType<Citation_Type>(_databaseContext.Server, new Citation_Type());
        }
    }
}
