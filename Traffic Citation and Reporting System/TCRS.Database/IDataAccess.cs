using System.Collections.Generic;
using System.Threading.Tasks;
using TCRS.Database.Model;

namespace TCRS.Database
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters, string connectionString);
        void SaveData<U>(string sql, U parameters, string connectionString);
        IEnumerable<T> GetAll<T>(string connectionString, T Model);
        Task<Person> GetUser(Person person, string connectionString);
        void SaveRefreshToken(RefreshToken refreshToken, string connectionString);
    }
}