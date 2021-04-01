using System.Collections.Generic;
using System.Threading.Tasks;

namespace TCRS.Database
{
    //Main
    public partial interface IDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters, string connectionString);
        IEnumerable<T> SyncLoadData<T, U>(string sql, U parameters, string connectionString);
        void SaveData<U>(string sql, U parameters, string connectionString);
        IEnumerable<T> GetAll<T>(string connectionString, T Model);
        int GetCount<T>(string sql, T parameters, string connectionString);

        void UpdateData<T>(string sql, T parameters, string connectionString);
    }
}
