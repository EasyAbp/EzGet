using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;

namespace EasyAbp.EzGet.NuGetPackages
{
    public class UnlistedNuGetPackageSpecification : Specification<NuGetPackage>
    {
        public override Expression<Func<NuGetPackage, bool>> ToExpression()
        {
            return p => p.Listed == false;
        }
    }
}
