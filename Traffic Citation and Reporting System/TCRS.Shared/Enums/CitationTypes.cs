using System.ComponentModel;

namespace TCRS.Shared.Enums
{
    public enum CitationTypes
    {
        //Citizen
        [Description("Running a red light")]
        RunningRed = 1,
        [Description("Seat belt violations")]
        SeatBeltViolation = 2,
        [Description("Failure to follow the right of way")]
        FailureToFollowRightOfWay = 3,
        [Description("Failure to signal when changing lanes")]
        FailureToSignalWhenChangingLanes = 4,
        [Description("Speeding")]
        Speeding = 5,
        [Description("Driving over a median")]
        DrivingOverAMedian = 6,
        [Description("Driving illegally")]
        DrivingIllegally = 7,
        [Description("Running over a pedestrian lane")]
        RunningOverAPedestrianLane = 8,
        [Description("Driving past a school bus when unloading/loading ")]
        DrivingPastASchoolBuswhenUnloadingLoading = 9,
        [Description("Failure to drive within a specific lane")]
        FailureToDriveWithinASpecifiedLane = 10,
        //Vehicle
        [Description("Fix it ticket")]
        FixItTicket = 11,
        [Description("Parking citation")]
        ParkingCitation = 12,
        [Description("Moving vehicle code warning")]
        MovingVehicleCodeWarning = 13,
        [Description("Moving vehicle code warning")]
        MovingVehicleCodeViolation = 14

    }
}
