using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Helpers
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id", Roles = "roles";

            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }

            public static class ClaimRole
            {
                public static IList<string> UserRoleClaims { get; set; } = new List<string>
                                                            {
                                                                "Admin",
                                                                "User"

                                                            };
            }

        }
    }
}