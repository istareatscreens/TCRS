using System.ComponentModel;

namespace TCRS.Shared.Enums
{
    public enum VehicleCitationTypes
    {
        //Vehicle
        [Description("Fix it ticket")]
        FixItTicket = 11,
        [Description("Parking citation")]
        ParkingCitation = 12,
        [Description("Moving vehicle code warning")]
        MovingVehicleCodeWarning = 13,
        [Description("Moving vehicle code violation")]
        MovingVehicleCodeViolation = 14
    }
}
