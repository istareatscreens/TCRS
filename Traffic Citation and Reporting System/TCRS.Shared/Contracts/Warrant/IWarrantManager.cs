using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Objects.Warrant;

namespace TCRS.Shared.Contracts.Warrant
{
    public interface IWarrantManager
    {
        Task<List<WarrantData>> GetWarrants(CreateWarrantObject createWarrantObject);
        Task RemoveWarrant(string reference_number);
        Task PostWarrant(CreateWarrantObject createWarrantObject);
    }
}
