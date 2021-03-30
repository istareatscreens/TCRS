using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Contracts.CitationManagement;
using TCRS.Shared.Objects.Citations;

namespace TCRS.Business
{
    public class CitationManager : ICitationManager
    {
        private readonly IPersistenceService _api;
        public CitationManager(IPersistenceService api)
        {
            _api = api;
        }
        public async Task<CitationIssuingDisplayData> IssueCitation(CitationIssueData citationIssueData)
        {
            IEnumerable<CitationIssuingDisplayData> cidd = await _api.PostAsync<CitationIssueData, CitationIssuingDisplayData>(citationIssueData);
            return cidd.ToList<CitationIssuingDisplayData>().FirstOrDefault();
        }
    }
}
