﻿using System.Collections.Generic;
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
        //Citations
        IEnumerable<Citation> GetCitationsByLicensePlate(string plate_number, string connectionString);
        IEnumerable<Citation> GetCitationByNumber(string citation_number, string connectionString);
        Task<IEnumerable<License_Plate>> GetVehicleInfoByLicencePlate(string licencePlate, string connectionString);
        IEnumerable<License> GetLicenseInfoByLicence(string license_id, string connectionString);
        void PostCitizenCitation(Citation Citation, License License, string connectionString);
        void PostVehicleCitation(Citation Citation, License_Plate License_Plate, string connectionString);

    }
}
