using System;
using System.Collections.Generic;
using System.Text;
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
        public void IssueCitation(CitationIssueData citationIssueData)
        {
            _api.PostAsync<CitationIssueData>(citationIssueData);
        }
    }
}
