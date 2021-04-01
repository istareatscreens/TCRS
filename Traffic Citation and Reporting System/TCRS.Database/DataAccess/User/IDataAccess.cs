using System.Threading.Tasks;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //User
    public partial interface IDataAccess
    {
        void SaveRefreshToken(RefreshToken refreshToken, string connectionString);
        Task<Person> GetUser(Person person, string connectionString);
    }
}
