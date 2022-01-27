using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.NuGet.Packages;
using EasyAbp.EzGet.PackageRegistrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.EntityFrameworkCore
{
    public static class EzGetEfCoreQueryableExtensions
    {
        public static IQueryable<PackageRegistration> IncludeDetails(this IQueryable<PackageRegistration> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable.Include(p => p.PackageRegistrationOwners);
        }

        public static IQueryable<NuGetPackage> IncludeDetails(this IQueryable<NuGetPackage> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(p => p.PackageTypes)
                .Include(p => p.Dependencies)
                .Include(p => p.TargetFrameworks);
        }

        public static IQueryable<Feed> IncludeDetails(this IQueryable<Feed> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable.Include(p => p.FeedCredentials);
        }
    }
}
