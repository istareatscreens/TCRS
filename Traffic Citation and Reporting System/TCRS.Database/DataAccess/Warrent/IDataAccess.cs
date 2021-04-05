using System.Collections.Generic;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //Wanted
    public partial interface IDataAccess
    {
        IEnumerable<Wanted_Vehicle> GetCitizenWarrentsByVehicle(int vehicle_id, string connectionString);
        IEnumerable<Wanted_Citizen> GetCitizenCitizenByVehicle(int citizen_id, string connectionString);
        void CreateCitizenWanted(Wanted Wanted, int citizen_id, string connectionString);
        void CreateVehicleWanted(Wanted Wanted, int vehicle_id, string connectionString);
        void ChangeWarrentStatus(int reference_no, bool active_status, string connectionString);
    }
}
