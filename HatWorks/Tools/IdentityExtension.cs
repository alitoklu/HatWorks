using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HatWorks.Tools
{
    public static class IdentityExtension
    {
        public static string Name(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).Claims.Where(c => c.Type == ClaimTypes.Name)
                              .Select(c => c.Value).SingleOrDefault();
            return (claim != null) ? claim : string.Empty;
        }

        public static string Companyid(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Companyid");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string Titleid(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("titleid");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string NameIdentifier(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                              .Select(c => c.Value).SingleOrDefault();
            return (claim != null) ? claim : string.Empty;
        }

        public static List<string> Role(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).Claims.Where(c => c.Type == ClaimTypes.Role)
                              .Select(c => c.Value).ToList();


            return claim;
        }

        //public static string ProfileImage(this IIdentity identity)
        //{
        //    var claim = ((ClaimsIdentity)identity).FindFirst("ProfileImage");
        //    return (claim != null) ? claim.Value : string.Empty;
        //}

        public static string Email(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).Claims.Where(c => c.Type == ClaimTypes.Email)
                              .Select(c => c.Value).SingleOrDefault();
            return (claim != null) ? claim : string.Empty;
        }
    }
}
