using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EzGet.EntityFrameworkCore
{
    public class EzGetModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public EzGetModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}