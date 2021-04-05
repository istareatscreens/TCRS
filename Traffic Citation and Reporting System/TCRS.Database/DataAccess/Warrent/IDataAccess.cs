using System.Collections.Generic;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //Wanted
    public partial interface IDataAccess
    {
        IEnumerable<Wanted_Vehicle> GetVehicleWarrants(int vehicle_id, string connectionString);
        IEnumerable<Wanted_Citizen> GetCitizenWarrants(int citizen_id, string connectionString);
        void CreateCitizenWanted(Wanted Wanted, int citizen_id, string connectionString);
        void CreateVehicleWanted(Wanted Wanted, int vehicle_id, string connectionString);
        void ChangeWarrantStatus(int reference_no, bool active_status, string connectionString);
    }
}
