
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace TCRS_db.Model
{
    public class Licence_Plate
    {
        [ExplicitKey]
        public int plate_number { get; set; }
        public int vehicle_id { get; set; }

        public bool expired { get; set; }

    }
}
