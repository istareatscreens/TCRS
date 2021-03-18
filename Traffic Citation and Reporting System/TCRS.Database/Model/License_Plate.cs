using Dapper.Contrib.Extensions;

namespace TCRS.Database.Model
{
    public class License_Plate
    {
        [ExplicitKey]
        public int plate_number { get; set; }
        public int vehicle_id { get; set; }

        public bool expired { get; set; }

        //Relationships
        //One to
        public Vehicle Vehicle { get; set; }

        //Many to
    }
}
