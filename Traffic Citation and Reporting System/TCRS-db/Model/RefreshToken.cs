using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCRS_db.Model
{
    public partial class RefreshToken
    {
        public int token_id { get; set; }
        public int person_id { get; set; }
        public string token { get; set; }
        public DateTime expiry_date { get; set; }

    }
}
