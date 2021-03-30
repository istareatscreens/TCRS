using System;

namespace TCRS.Shared.Objects.Citations
{
    public class CitizenVehicleCitation
    {
        // Get request
        // Belongs in CitationResolution
        // Takes in parameter(vehicle_id)
        public int citation_id { get; set; }
        public string citation_number { get; set; }
        public string name { get; set; }
        public DateTime date_recieved { get; set; }
        public bool training_eligable { get; set; }

        public int vehicle_id { get; set; }
        public double fine { get; set; }
        public string plate_number { get; set; }
    }
}
