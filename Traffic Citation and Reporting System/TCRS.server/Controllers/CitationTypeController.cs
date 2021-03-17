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
    [Route("Citation")]
    public class CitationTypeController : Controller
    {
        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;

        public CitationTypeController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }

        [HttpGet("PlateNumberCitations")]
        public  IEnumerable<CitizenVehicleCitation> GetCitationByLicencePlateAsync()
        {
            //?licenceplate=23423414
            try
            {
                var citations = new List<CitizenVehicleCitation>();
                foreach (var citation in _db.GetCitationsByLicencePlate("55870382", _databaseContext.Server))
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
                            training_eligable = citation.Citation_Type.training_eligable
                        }
                        );

                };

                return citations;
            }catch(Exception e)
            {
                return (IEnumerable<CitizenVehicleCitation>)NotFound("Not found");
            }
        }

        [HttpGet]
       // [Authorize(Roles = Roles.Manager)]
        public IEnumerable<Citation_Type> GetCitationType()
        {     
            return _db.GetAllCitationType<Citation_Type>(_databaseContext.Server, new Citation_Type());
        }
    }
}
