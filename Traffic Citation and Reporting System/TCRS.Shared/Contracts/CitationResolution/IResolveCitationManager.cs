using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Objects.CitationResolution;

namespace TCRS.Shared.Contracts
{
    public interface IResolveCitationManager
    {
        Task<List<CitationResolutionLoginData>> CitizenLogin(CitationResolutionLoginData citationResolutionLoginData);
    }
}
