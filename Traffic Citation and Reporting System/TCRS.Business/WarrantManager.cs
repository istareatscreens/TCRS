using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<List<WarrantData>> GetWarrants(string licence_id)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("licence_id", licence_id));

            var data = await _api.GetAsync<WarrantData>(parameters);
            return data.ToList();
        }

        public async Task PostWarrant(CreateWarrantObject createWarrantObject)
        {
            await _api.PostAsync<CreateWarrantObject>(createWarrantObject);
        }

        public async Task RemoveWarrant(string reference_number)
        {
            await _api.PostAsync<DeleteWarrantObject>(new DeleteWarrantObject { reference_number = reference_number });
        }
    }
}
