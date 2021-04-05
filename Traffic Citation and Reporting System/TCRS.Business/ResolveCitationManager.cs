using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.CitationResolution;
using TCRS.Shared.Objects.Citations;
using TCRS.Shared.Objects.CourseEnrollment;
using TCRS.Shared.Objects.Payment;

namespace TCRS.Business
{
    public class ResolveCitationManager : IResolveCitationManager
    {
        private readonly IPersistenceService _api;
        public ResolveCitationManager(IPersistenceService api)
        {
            _api = api;
        }
        public async Task<List<CitizenVehicleCitation>> CitizenLogin(CitationResolutionLoginData citationResolutionLoginData)
        {

            var parameters = new List<KeyValuePair<string, string>>();

            if (citationResolutionLoginData.citation_number != "")
            {
                parameters.Add(new KeyValuePair<string, string>("citation_number", citationResolutionLoginData.citation_number));
            }
            else
            {
                throw new NullReferenceException();
            }

            var crld = await _api.GetAsync<CitizenVehicleCitation>(parameters);
            return crld.ToList();
        }

        public async Task MakePayment(PaymentData paymentData)
        {
            await _api.PostAsync<PaymentData>(paymentData);
        }
    }
}
