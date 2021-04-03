﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Objects.CitationResolution;
using TCRS.Shared.Objects.Citations;

namespace TCRS.Shared.Contracts
{
    public interface IResolveCitationManager
    {
        Task<List<CitizenVehicleCitation>> CitizenLogin(CitationResolutionLoginData citationResolutionLoginData);
    }
}
