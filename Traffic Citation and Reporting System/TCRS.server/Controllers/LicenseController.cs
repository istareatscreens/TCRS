﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using TCRS.Database;
using TCRS.Server.Tokens;
using TCRS.Shared.Objects.Lookup;
using TCRS.Shared.Objects.Warrant;

namespace TCRS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : Controller
    {
        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;
        public LicenseController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LookupCitizenDisplayData>> GetCitizenInfoByLicenseID([FromQuery] string license_id)
        {
            //Return type is wrapped in action result to allow NotFond to be returned
            //I pull licenseplate url query parameter here to be passed to database query
            if (license_id == null)
            {
                return NotFound("Citizen with this License ID does not exist");
            }
            try
            {
                var citizenData = _db.GetCitizenInfoByLicenseID(license_id, _databaseContext.Server);
                var citizenWantedList = _db.GetWantedCitizenInfoByCitizenId(citizenData.ToList().FirstOrDefault().citizen_id, _databaseContext.Server);
                var citationData = _db.GetCitationsByLicense(license_id, _databaseContext.Server);
                var citizen = citizenData.ToList().Select(citizen => new LookupCitizenDisplayData
                {
                    first_name = citizen.Citizen.first_name,
                    middle_name = citizen.Citizen.middle_name,
                    last_name = citizen.Citizen.last_name,
                    license_id = citizen.license_id,
                    expiration_date = citizen.expiration_date,
                    is_revoked = citizen.is_revoked,
                    is_suspended = citizen.is_suspended,
                    license_class = citizen.license_class,

                    CitizenWantedData = (citizenWantedList != null) ? citizenWantedList.Select(record => new WarrantData
                    {
                        reference_number = record.Wanted.reference_no,
                        dangerous = record.Wanted.dangerous,
                        crime = record.Wanted.crime,
                        status = record.Wanted.active_status
                    }) : null
                }
            );
                return Ok(citizen);
            }
            catch (Exception)
            {
                return NotFound("Not found");
            }
        }

        [HttpGet("GetVehicleInfoByLicensePlate")]
        public ActionResult<IEnumerable<LookupVehicleDisplayData>> GetVehicleInfoByLicensePlate([FromQuery] string plate_number)

        {
            //Return type is wrapped in action result to allow NotFond to be returned

            //I pull licenseplate url query parameter here to be passed to database query
            if (plate_number == null)
            {
                return NotFound("Vehicle with this Plate number does not exist");
            }
            try
            {
                var vehicles = new List<LookupVehicleDisplayData>();
                foreach (var vehicle in _db.GetVehicleInfoByLicensePlate(plate_number, _databaseContext.Server))
                {
                    vehicles.Add(
                        new LookupVehicleDisplayData
                        {
                            vehicle_id = vehicle.Vehicle.vehicle_id,
                            vin = vehicle.Vehicle.vin,
                            name = vehicle.Vehicle.name,
                            stolen = vehicle.Vehicle.stolen,
                            make = vehicle.Vehicle.make,
                            registered = vehicle.Vehicle.registered,
                            model = vehicle.Vehicle.model,
                            year_made = vehicle.Vehicle.year_made,
                            citizen_id = vehicle.Vehicle.citizen_id,
                            insurer_id = vehicle.Vehicle.insurer_id,
                        }
                        );

                };

                return vehicles;
            }
            catch (Exception)
            {
                return NotFound("Not found");
            }
        }
    }
}
