using System.Collections.Generic;
using System.Threading.Tasks;
using TCRS_db.Model;

namespace TCRS_db
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters, string connectionString);
        void SaveData<U>(string sql, U parameters, string connectionString);
        IEnumerable<T> GetAll<T>(string connectionString, T Model);
        Task<Person> GetUser(Person person, string connectionString);
    }
}