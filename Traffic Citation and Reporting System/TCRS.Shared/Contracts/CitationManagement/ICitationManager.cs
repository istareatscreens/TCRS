using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Objects.Citations;

namespace TCRS.Shared.Contracts.CitationManagement
{
    public interface ICitationManager
    {
        public Task<CitationIssuingDisplayData> IssueCitation(CitationIssueData citationIssueData);

    }
}
