using System;
using System.Collections.Generic;
using TCRS.Shared.Objects.Auth;
using TCRS.Shared.Objects.CitationResolution;
using TCRS.Shared.Objects.Citations;
using TCRS.Shared.Objects.CourseEnrollment;
using TCRS.Shared.Objects.CourseManagement;
using TCRS.Shared.Objects.EmployeeLookup;
using TCRS.Shared.Objects.Lookup;
using TCRS.Shared.Objects.Payment;

namespace TCRS.APIAccess
{
    class RouteByType
    {

        public static Dictionary<Type, string> PostEntityRouteAssignment { get; } = new Dictionary<Type, string>
        {
            /* ***** Citations ***** */
            // Returns CitationIssueData
            {typeof(CitationIssueData), "api/Citations/IssueCitation"},

            /* ***** CitationResolution ***** */
            // citation_id XOR plate_number should be passed
            {typeof(CitationResolutionLoginData), "api/Citations/Login" },

            /* ***** CourseEnrollmentBookingData ***** */
            // CourseEnrollmentBookingDataController : JWT for current signed in citizen_id should be passed
            {typeof(CourseEnrollmentBookingData), "api/CourseEnrollment/Book" },

            /* ***** CourseManagement ***** */
            // JWT should be passed for the logged in instructor's id
            {typeof(CourseManagementData), "api/Course/SubmitCourse" },

            /* ***** EmployeeLookup ***** */
            // Returns EmployeeLookupData 
            {typeof(EmployeeLookupData), "api/EmployeeLookup/Lookup" },

            /* ***** Payment ***** */
            // citation_id should be passed
            {typeof(PaymentData), "api/Payment/SubmitPayment" }

        };
        public static Dictionary<Type, string> GetEntityRouteAssignment { get; } = new Dictionary<Type, string>
        {
            /* ***** Citations ***** */
            {typeof(CitizenVehicleCitation), "api/Citations"},
            {typeof(User), "api/Users/getUser"},

            /* ***** CourseEnrollmentData ***** */
            // CourseEnrollmentDataController : get(course_id, citation_id)
            {typeof(CourseEnrollmentData), "api/CourseEnrollmentData" },

            /* ***** CitationResolution ***** */
            // CitationResolutionController: get(citation_id)
            {typeof(CitationResolutionData), "api/Citations" },

            /* ***** EmployeeLookup ***** */
            // EmployeeLookupData: get()
            {typeof(EmployeeLookupData), "api/Employee" },

            /* ***** LookupPortal ***** */
            // CitationController: get(citation_id)
            {typeof(LookupCitationDisplayData), "api/Citations" },

            // CitizenController: get(licence_id)
            {typeof(LookupCitizenDisplayData), "api/Citizen" },

            // VehicleController: get(vehicle_id)
            {typeof(LookupVehicleDisplayData), "api/Vehicle" },

        };

    }
}
