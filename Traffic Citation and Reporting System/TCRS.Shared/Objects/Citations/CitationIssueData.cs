﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TCRS.Shared.Objects.Citations
{
    public class CitationIssueData
    {

        public int person_id { get; set; }
        public int citation_type_id { get; set; }
#nullable enable
        public string? license_id { get; set; }
        public String? licencePlate { get; set; }
        
    }
}
