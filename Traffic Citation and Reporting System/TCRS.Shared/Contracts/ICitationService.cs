using System.Collections.Generic;
using TCRS.Shared.Objects.Citations;

namespace TCRS.Shared.Contracts
{
    public interface ICitationService
    {
        List<CitizenVehicleCitation> GetCitizenVehicleCitations();
        void SetCitizenVehicleCitations(List<CitizenVehicleCitation> CitizenVehicleCitation);
    }
}
