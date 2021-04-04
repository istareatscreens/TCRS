using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Objects.EmployeeLookup;

namespace TCRS.Shared.Contracts.EmployeeLookup
{
    public interface IEmployeeLookupManager
    {
        Task<List<Employee>> GetEmployeeNames();
        Task<List<EmployeeLookupData>> GetEmployeeLookup(DateTime start_date, DateTime end_date);
    }
}
