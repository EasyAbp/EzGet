using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;

namespace EasyAbp.EzGet.NuGetPackages
{
    public class UniqueNuGetPackageSpecification : Specification<NuGetPackage>
    {
        protected string PackageName { get; }
        protected string Version { get; }

        public UniqueNuGetPackageSpecification(string packageName, string version)
        {
            PackageName = packageName;
            Version = version;
        }

        public override Expression<Func<NuGetPackage, bool>> ToExpression()
        {
            return p => p.PackageName == PackageName && p.NormalizedVersion == Version;
        }
    }
}
