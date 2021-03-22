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
using TCRS.Shared.Objects.PoliceDepartment;

namespace TCRS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/GetPoliceDepartmentEmployeesByManager")]
    public class PoliceDepartmentController : Controller
    {
        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;

        public PoliceDepartmentController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PoliceDept>> GetPoliceDeptEmployeesByManager([FromQuery] int manager_id)
        {
            if (manager_id == 0)
            {
                return NotFound("No Manager with this ID found");
            }
            try
            {
                var employees = new List<PoliceDept>();
                foreach (var employee in _db.GetPoliceDeptEmployeesByManager(manager_id, _databaseContext.Server))
                {
                    employees.Add(
                        new PoliceDept
                        {
                            police_dept_id = employee.police_dept_id,
                            manager_id = employee.manager_id,
                            name = employee.name,
                            person_id = employee.Persons.person_id,
                            first_name = employee.Persons.first_name,
                            last_name = employee.Persons.last_name,
                            email = employee.Persons.email
                        }
                        );

                };

                return employees;

            }
            catch (Exception e)
            {
                return NotFound("Not found");
            }
        }
    }
}
