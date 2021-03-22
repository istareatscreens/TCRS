using System;
using System.Collections.Generic;
using System.Text;
using TCRS.Shared.Objects.Citations;

namespace TCRS.Shared.Contracts.CitationManagement
{
    public interface ICitationManager
    {
        void IssueCitation(CitationIssueData citationIssueData);
    }
}
