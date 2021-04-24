using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using TCRS.Database;
using TCRS.Database.Model;
using TCRS.Server.Tokens;
using TCRS.Shared.Enums;
using TCRS.Shared.Objects.Auth;
using TCRS.Shared.Objects.EmployeeLookup;

namespace TCRS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;
        public EmployeeController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Manager)]
        [Authorize(Roles = Roles.HighwayPatrolOfficer + "," + Roles.MunicipalOfficer)]
        public ActionResult<IEnumerable<EmployeeLookupData>> GetEmployeeData([FromHeader] string authorization, [FromQuery] DateTime start_date, [FromQuery] DateTime end_date)
        {
            var User = new User(authorization);

            IEnumerable<Police_Dept> PoliceEmployee = null;
            IEnumerable<Municipality> MunicipalEmployee = null;
            try
            {


                if (User.isManager && User.isHighway_Patrol_Officer)
                {
                    PoliceEmployee = _db.GetPoliceDeptEmployeesByManager(User.person_id, _databaseContext.Server);
                }

                else if (User.isManager && User.isMunicipal_Officer)
                {
                    MunicipalEmployee = _db.GetMunicipalOfficersByManager(User.person_id, _databaseContext.Server);
                }

                else
                {
                    return BadRequest(new { message = "Invalid User Credentials" });
                }

                Func<int, List<KeyValuePair<int, int>>> GetCitationCount = (int person_id) =>
                {
                    var CitationCountbyType = new List<KeyValuePair<int, int>>();
                    var sum = 0;
                    foreach (CitationTypes item in CitationTypes.GetValues(typeof(CitationTypes)))
                    {
                        var count = _db.GetCitationCountforPersonbyTypeId(person_id, (int)item, start_date, end_date, _databaseContext.Server);
                        sum += count;
                        CitationCountbyType.Add(new KeyValuePair<int, int>((int)item, count));
                    }
                    CitationCountbyType.Add(new KeyValuePair<int, int>(0, sum)); //total
                    return CitationCountbyType;
                };


                if (PoliceEmployee != null)
                {
                    return Ok(PoliceEmployee.ToList().Select(employee => new EmployeeLookupData
                    {
                        first_name = employee.Persons.first_name,
                        last_name = employee.Persons.last_name,
                        email = employee.Persons.email,
                        active = employee.Persons.active,
                        police_dept_id = employee.police_dept_id,
                        police_dept_name = PoliceEmployee.ToList().FirstOrDefault().name,
                        CitationCountbyType = GetCitationCount(employee.Persons.person_id)
                    })) ;
                }

                else if (MunicipalEmployee != null)
                {
                    return Ok(MunicipalEmployee.ToList().Select(employee => new EmployeeLookupData
                    {
                        first_name = employee.Person.first_name,
                        last_name = employee.Person.last_name,
                        email = employee.Person.email,
                        active = employee.Person.active,
                        munic_id = employee.munic_id,
                        municipality_name = MunicipalEmployee.ToList().FirstOrDefault().name,
                        CitationCountbyType = GetCitationCount(employee.Person.person_id)
                    }));
                }
            }
            catch
            {
                return BadRequest(new { message = "Request Error" });
            }
            return BadRequest(new { message = "Unreachable Error" });
        }

        [HttpGet("EmployeeNames")]
        [Authorize(Roles = Roles.Manager)]
        [Authorize(Roles = Roles.HighwayPatrolOfficer + "," + Roles.MunicipalOfficer)]
        public ActionResult<IEnumerable<Employee>> GetEmployeeName([FromHeader] string authorization)
        {
            var User = new User(authorization);
            IEnumerable<Police_Dept> PoliceEmployee = null;
            IEnumerable<Municipality> MunicipalEmployee = null;

            try
            {
                if (User.isManager && User.isHighway_Patrol_Officer)
                {
                    PoliceEmployee = _db.GetPoliceDeptEmployeesByManager(User.person_id, _databaseContext.Server);

                    return Ok(PoliceEmployee.ToList().Select(employee => new Employee
                    {
                        first_name = employee.Persons.first_name,
                        last_name = employee.Persons.last_name
                    }));
                }

                else if (User.isManager && User.isMunicipal_Officer)
                {
                    MunicipalEmployee = _db.GetMunicipalOfficersByManager(User.person_id, _databaseContext.Server);

                    return Ok(MunicipalEmployee.ToList().Select(employee => new Employee
                    {
                        first_name = employee.Person.first_name,
                        last_name = employee.Person.last_name
                    }));
                }
            }
            catch
            {
                return BadRequest(new { message = "Bad Request" });
            }
            return BadRequest(new { message = "Unreachable Error" });
        }


        [HttpGet("GetCitationsByOfficer")]
        [Authorize(Roles = Roles.Manager)]
        [Authorize(Roles = Roles.HighwayPatrolOfficer + "," + Roles.MunicipalOfficer)]
        public ActionResult<IEnumerable<EmployeeCitationsLookup>> GetCitationIssuedByOfficers([FromQuery] int person_id)
        {
            //Return type is wrapped in action result to allow NotFond to be returned

            //I pull licenseplate url query parameter here to be passed to database query
            if (person_id == 0 || person_id > 1000)
            {
                return NotFound(new { message = "Employee with this ID does not exist" });
            }
            try
            {
                var citations = new List<EmployeeCitationsLookup>();
                foreach (var citation in _db.GetCitationIssuedByOfficers(person_id, _databaseContext.Server))
                {
                    citations.Add(
                        new EmployeeCitationsLookup
                        {
                            person_id = citation.person_id,
                            first_name = citation.first_name,
                            last_name = citation.last_name,
                            email = citation.email,
                            citation_number = citation.Citation.citation_number,
                            date_recieved = citation.Citation.date_recieved,
                            name = citation.Citation_Type.name,
                            fine = citation.Citation_Type.fine,
                            training_eligable = citation.Citation_Type.training_eligable,
                            due_date_month = citation.Citation_Type.due_date_month
                        }
                        );
                };

                return citations;
            }
            catch (Exception)
            {
                return NotFound(new { message = "Not found" });
            }
        }
    }
}
