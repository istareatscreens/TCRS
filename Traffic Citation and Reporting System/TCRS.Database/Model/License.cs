using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCRS.Database.Model
{
    public class License
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string license_id { get; set; }

        [Key]
        public int citizen_id { get; set; }
        public DateTime expiration_date { get; set; }
        public bool is_revoked { get; set; }
        public bool is_suspended { get; set; }
        public string license_class { get; set; }

        //Relationship 
        //One to
        public Citizen Citizen { get; set; }

        //Many to
        public ICollection<Vehicle> Vehicles { get; set; }

    }
}
