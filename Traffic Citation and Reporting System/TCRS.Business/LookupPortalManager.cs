using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Contracts.LookupPortal;
using TCRS.Shared.Objects.Citations;
using TCRS.Shared.Objects.Lookup;

namespace TCRS.Business
{
    public class LookupPortalManager : ILookupPortalManager
    {
        private readonly IPersistenceService _api;
        public LookupPortalManager(IPersistenceService api)
        {
            _api = api;
        }

        public async Task<LookupCitationDisplayData> LookupCitationData(string citation_number)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("citation_number", citation_number));
            var data = await _api.GetAsync<LookupCitationDisplayData>(parameters);
            return data.ToList().FirstOrDefault();
        }

        public async Task<LookupCitizenDisplayData> LookupCitizenData(string licence_id)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("license_id", licence_id));
            var data = await _api.GetAsync<LookupCitizenDisplayData>(parameters);
            return data.ToList().FirstOrDefault();
        }

        public async Task<LookupVehicleDisplayData> LookupVehicleData(string plate_number)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("plate_number", plate_number));
            var data = await _api.GetAsync<LookupVehicleDisplayData>(parameters);
            return data.ToList().FirstOrDefault();
        }

        public async Task ResolveCitation(RemoveCitationObject removeCitationObject)
        {
            await _api.PutAsync<RemoveCitationObject>(removeCitationObject);
        }
    }
}
