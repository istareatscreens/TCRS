using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Citations;

namespace TCRS.Business
{
    public class CitationService : ICitationService
    {

        public List<CitizenVehicleCitation> GetCitizenVehicleCitations() 
        {
            return CitizenVehicleCitations;
        }
        public List<CitizenVehicleCitation> CitizenVehicleCitations { get; set; }

    }
}
