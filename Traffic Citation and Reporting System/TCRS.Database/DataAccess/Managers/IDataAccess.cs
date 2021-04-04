using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Database.Model;

//Managers
namespace TCRS.Database
{
    public partial interface IDataAccess
    {
        IEnumerable<Police_Dept> GetPoliceDeptEmployeesByManager(int manager_id, string connectionString);
        IEnumerable<Municipality> GetMunicipalOfficersByManager(int manager_id, string connectionString);
        IEnumerable<Person> GetCitationIssuedByOfficers(int person_id, string connectionString);

        int GetCitationCountforPersonbyTypeId(int person_id, int citation_type_id, DateTime start_date, DateTime end_date, string connectionString);
    }
}
