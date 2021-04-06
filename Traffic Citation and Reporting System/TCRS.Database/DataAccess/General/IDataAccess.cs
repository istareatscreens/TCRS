using System.Collections.Generic;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //General
    public partial interface IDataAccess
    {
        IEnumerable<Citizen> GetCitizenById(int citizen_id, string connectionString);
    }
}
