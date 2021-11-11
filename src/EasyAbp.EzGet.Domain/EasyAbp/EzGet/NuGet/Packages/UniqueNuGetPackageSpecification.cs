using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;

namespace EasyAbp.EzGet.NuGet.Packages
{
    public class UniqueNuGetPackageSpecification : Specification<NuGetPackage>
    {
        protected string PackageName { get; }
        protected string Version { get; }
        protected Guid? FeedId { get; }

        public UniqueNuGetPackageSpecification(string packageName, string version, Guid? feedId)
        {
            PackageName = packageName;
            Version = version;
            FeedId = feedId;
        }

        public override Expression<Func<NuGetPackage, bool>> ToExpression()
        {
            return p => p.PackageName == PackageName && p.NormalizedVersion == Version && p.FeedId == FeedId;
        }
    }
}
