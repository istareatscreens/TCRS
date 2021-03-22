using System;
using System.Collections.Generic;
using System.Text;
using TCRS.Shared.Objects.Auth;
using TCRS.Shared.Objects.Citations;

namespace TCRS.APIAccess
{
    class RouteByType
    {
        public static Dictionary<Type, string> GetEntityRouteAssignment { get; } = new Dictionary<Type, string>
        {
            {typeof(CitizenVehicleCitation), "api/Citations"},
            {typeof(User), "api/Users/getUser"}
        };

    }
}
