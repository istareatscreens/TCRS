using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Contracts.Warrant;
using TCRS.Shared.Objects.Warrant;

namespace TCRS.Business
{
    public class WarrantManager : IWarrantManager
    {
        private readonly IPersistenceService _api;
        public WarrantManager(IPersistenceService api)
        {
            _api = api;
        }

        public async Task<List<WarrantData>> GetWarrants(CreateWarrantObject createWarrantObject)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (createWarrantObject.license_id != null)
            {
                parameters.Add(new KeyValuePair<string, string>("license_id", createWarrantObject.license_id));
            }
            else if(createWarrantObject.plate_number != null)
            {
                parameters.Add(new KeyValuePair<string, string>("plate_number", createWarrantObject.plate_number));
            }
            else
            {
                throw new Exception("No licence_id or plate_number specified");
            }
            
            var data = await _api.GetAsync<WarrantData>(parameters);
            return data.ToList();
        }

        public async Task PostWarrant(CreateWarrantObject createWarrantObject)
        {
            await _api.PostAsync<CreateWarrantObject>(createWarrantObject);
        }

        public async Task RemoveWarrant(string reference_no)
        {
            await _api.PutAsync<DeleteWarrantObject>(new DeleteWarrantObject { reference_no = reference_no });
        }
    }
}
