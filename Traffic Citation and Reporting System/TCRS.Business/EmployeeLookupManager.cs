using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Contracts.EmployeeLookup;
using TCRS.Shared.Objects.EmployeeLookup;

namespace TCRS.Business
{
    public class EmployeeLookupManager : IEmployeeLookupManager
    {
        private readonly IPersistenceService _api;
        public EmployeeLookupManager(IPersistenceService api)
        {
            _api = api;
        }

        public async Task<List<EmployeeLookupData>> GetEmployeeLookup()
        {
            var empData = await _api.GetAsync<EmployeeLookupData>();
            return empData.ToList();
        }

        public async Task<List<Employee>> GetEmployeeNames()
        {
            var empData = await _api.GetAsync<Employee>();
            return empData.ToList();
        }
    }
}
