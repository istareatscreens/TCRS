using System.Collections.Generic;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Citations;

namespace TCRS.Business
{
    public class CitationService : ICitationService
    {

        public List<CitizenVehicleCitation> CitizenVehicleCitations = null;

        public void SetCitizenVehicleCitations(List<CitizenVehicleCitation> CitizenVehicleCitation)
        {
            this.CitizenVehicleCitations = CitizenVehicleCitation;
        }
        public List<CitizenVehicleCitation> GetCitizenVehicleCitations()
        {
            if (CitizenVehicleCitations != null)
            {
                return CitizenVehicleCitations;
            }
            else
            {
                /*
                var citizenVehicleCitations = new List<CitizenVehicleCitation>();
                citizenVehicleCitations.Add(new CitizenVehicleCitation());
                return citizenVehicleCitations;
                */
                return null;
            }
        }


    }
}
