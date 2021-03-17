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
        IEnumerable<T> GetAllCitationType<T>(string connectionString, T Model);
        void SaveRefreshToken(RefreshToken refreshToken, string connectionString);
#nullable enable
       IEnumerable<Citation> GetCitationsByLicencePlate(string licence_plate, string connectionString);
    }
}