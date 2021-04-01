using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.CitationResolution;

namespace TCRS.Business
{
    public class ResolveCitationManager : IResolveCitationManager
    {
        private readonly IPersistenceService _api;
        public ResolveCitationManager(IPersistenceService api)
        {
            _api = api;
        }
        public async Task<List<CitationResolutionLoginData>> CitizenLogin(CitationResolutionLoginData citationResolutionLoginData)
        {

            var parameters = new List<KeyValuePair<string, string>>();

            if(citationResolutionLoginData.plate_number != null)
            {
                parameters.Add(new KeyValuePair<string, string>("plate_number", citationResolutionLoginData.plate_number));
            }
            else if (citationResolutionLoginData.licence_number != null)
            {
                parameters.Add(new KeyValuePair<string, string>("licence_number", citationResolutionLoginData.licence_number));
            }
            else
            {
                throw new NullReferenceException();
            }

            var crld = await _api.GetAsync<CitationResolutionLoginData>(parameters);
            return crld.ToList();
        }
    }
}
