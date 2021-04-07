using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Objects.Citations;
using TCRS.Shared.Objects.Lookup;

namespace TCRS.Shared.Contracts.LookupPortal
{
    public interface ILookupPortalManager
    {
        Task<LookupCitationDisplayData> LookupCitationData(string citation_number);
        Task<LookupCitizenDisplayData> LookupCitizenData(string licence_id);
        Task<LookupVehicleDisplayData> LookupVehicleData(string plate_number);
        Task ResolveCitation(RemoveCitationObject removeCitationObject);

    }
}
