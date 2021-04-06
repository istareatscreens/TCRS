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
        List<WarrantData> GetWarrants();
        
    }
}
