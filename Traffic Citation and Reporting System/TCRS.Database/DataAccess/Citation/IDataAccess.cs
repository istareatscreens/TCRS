using System.Collections.Generic;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //Citations
    public partial interface IDataAccess
    {
        void PostCitizenCitation(Citation Citation, License License, string connectionString);
        void PostVehicleCitation(Citation Citation, License_Plate License_Plate, string connectionString);
        IEnumerable<Citation> GetCitationsByLicensePlate(string plate_number, string connectionString);
        IEnumerable<Citation> GetCitationsByLicense(string license_id, string connectionString);
        IEnumerable<Citation> GetCitationByNumber(string citation_number, string connectionString);
        IEnumerable<T> GetAllCitationType<T>(string connectionString, T Model);
        IEnumerable<Citation_Type> GetCitationTypeById(int citation_type_id, string connectionString);
        bool CheckIfCitationIsResolved(int citation_id, string connectionString);
        void UpdateCitationToResolved(int citation_id, string connectionString);
        bool CitationBelongsToVehicle(int citation_id, int vehicle_id, string connectionString);
        bool CitationBelongsToCitizen(int citation_id, int citizen_id, string connectionString);
        bool CitationIsRegisteredToCourse(int citation_id, string connectionString);
        void PayForCitation(Payment Payment, string connectionString);
        public IEnumerable<Citation> GetCitationAllInformationByNumber(string citation_number, string connectionString);
    }
}
