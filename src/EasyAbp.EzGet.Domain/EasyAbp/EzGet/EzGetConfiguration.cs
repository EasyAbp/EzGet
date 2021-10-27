using EasyAbp.EzGet.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.EzGet
{
    public class EzGetConfiguration : IEzGetConfiguration, ITransientDependency
    {
        protected ISettingProvider SettingProvider { get; }

        public EzGetConfiguration(ISettingProvider settingProvider)
        {
            SettingProvider = settingProvider;
        }

        public async Task<string> GetHostUrlAsync()
        {
            var hostUrl = await SettingProvider.GetOrNullAsync(EzGetSettingNames.HostUrl);

            if (!hostUrl.EndsWith("/"))
            {
                hostUrl = hostUrl + "/";
            }

            return hostUrl;
        }
    }
}
