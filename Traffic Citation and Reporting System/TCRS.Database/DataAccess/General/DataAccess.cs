using System.Collections.Generic;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //General
    public partial class DataAccess
    {
        public IEnumerable<Citizen> GetCitizenById(int citizen_id, string connectionString)
        {
            return SyncLoadData<Citizen, Citizen>(@"SELECT * FROM citizen WHERE citizen_id = @citizen_id", new Citizen { citizen_id = citizen_id }, connectionString);
        }


    }
}
