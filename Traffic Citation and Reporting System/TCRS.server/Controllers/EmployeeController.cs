using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Database;
using TCRS.Server.Tokens;
using TCRS.Shared.Objects.EmployeeLookup;
using TCRS.Shared.Objects.Auth;
using TCRS.Database.Model;

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
        public ActionResult<IEnumerable<EmployeeLookupData>> GetEmployeeData([FromHeader] string authorization)
        {
            var User = new User(authorization);

            IEnumerable<EmployeeLookupData> EmployeeData = null;
            IEnumerable<Police_Dept> PoliceEmployee  = null;
            IEnumerable<Municipality> MunicipalEmployee = null;


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
                return BadRequest("Invalid User Credentials");
            }

            if(PoliceEmployee != null)
            {
                var police = PoliceEmployee.ToList().Select(employee => new EmployeeLookupData { 
                    first_name = employee.Persons.first_name,
                    last_name = employee.Persons.last_name,
                    email = employee.Persons.email,
                    active = employee.Persons.active,
                    police_dept_id = employee.police_dept_id
                });

                return police;
            }

            else if(MunicipalEmployee != null)
            {
                var municipal = MunicipalEmployee.ToList().Select(emp => new EmployeeLookupData
                {
                    first_name = emp.Person.first_name,
                    last_name = emp.Person.last_name,
                    email = emp.Person.email,
                    active = emp.Person.active,
                    munic_id = emp.munic_id
                });

                return municipal;
            }
        }
    }
}
