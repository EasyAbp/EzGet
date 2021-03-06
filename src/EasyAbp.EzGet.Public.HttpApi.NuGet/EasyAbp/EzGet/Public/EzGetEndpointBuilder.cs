using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EzGet.Public
{
    public class EzGetEndpointBuilder : ITransientDependency
    {
        protected IOptions<FeedOptions> Options { get; }

        private const string _asyncSuffix = "Async";
        private const string _controllerSuffix = "Controller";

        public EzGetEndpointBuilder(IOptions<FeedOptions> options)
        {
            Options = options;
        }

        public virtual void MapEzGetEndpoints(IEndpointRouteBuilder endpoints)
        {
            MapPackagePublishRoutes(endpoints);
            MapPackageContentRoutes(endpoints);
            MapPackageSearchRoutes(endpoints);
            MapRegistrationIndexRoutes(endpoints);
            MapServiceIndexRoutes(endpoints);
            MapAutocompleteRoutes(endpoints);
        }

        private void MapAutocompleteRoutes(IEndpointRouteBuilder endpoints)
        {
            MapControllerRoute(
                endpoints,
                EzGetRoutesName.ServiceIndex.Get,
                ServiceIndexUrlConsts.AutocompleteUrl,
                nameof(AutocompleteApiController),
                nameof(AutocompleteApiController.GetAsync),
                "GET");
        }

        private void MapServiceIndexRoutes(IEndpointRouteBuilder endpoints)
        {
            MapControllerRoute(
                endpoints,
                EzGetRoutesName.ServiceIndex.Get,
                ServiceIndexUrlConsts.ServiceIndexUrl,
                nameof(ServiceIndexApiController),
                nameof(ServiceIndexApiController.GetAsync),
                "GET");
        }

        private void MapRegistrationIndexRoutes(IEndpointRouteBuilder endpoints)
        {
            MapControllerRoute(
                endpoints,
                EzGetRoutesName.RegistrationIndex.GetIndex,
                $"{ServiceIndexUrlConsts.RegistrationsBaseUrlUrl}/{{id}}/index.json",
                nameof(RegistrationIndexApiController),
                nameof(RegistrationIndexApiController.GetIndexAsync),
                "GET");

            MapControllerRoute(
                endpoints,
                EzGetRoutesName.RegistrationIndex.GetLeaf,
                $"{ServiceIndexUrlConsts.RegistrationsBaseUrlUrl}/{{id}}/{{version}}.json",
                nameof(RegistrationIndexApiController),
                nameof(RegistrationIndexApiController.GetLeafAsync),
                "GET");
        }

        private void MapPackageSearchRoutes(IEndpointRouteBuilder endpoints)
        {
            MapControllerRoute(
                endpoints,
                EzGetRoutesName.PackageSearch.GetList,
                ServiceIndexUrlConsts.SearchQueryServiceUrl,
                nameof(PackageSearchApiController),
                nameof(PackageSearchApiController.GetAsync),
                "GET");
        }

        private void MapPackageContentRoutes(IEndpointRouteBuilder endpoints)
        {
            MapControllerRoute(
                endpoints,
                EzGetRoutesName.PackageContent.GetVersions,
                $"{ServiceIndexUrlConsts.PackageBaseAddressUrl}/{{id}}/index.json",
                nameof(PackageContentApiController),
                nameof(PackageContentApiController.GetVersionsAsync),
                "GET");

            MapControllerRoute(
                endpoints,
                EzGetRoutesName.PackageContent.GetPackageContent,
                $"{ServiceIndexUrlConsts.PackageBaseAddressUrl}/{{id}}/{{version}}/{{idDotVersion}}.nupkg",
                nameof(PackageContentApiController),
                nameof(PackageContentApiController.GetPackageContentAsync),
                "GET");

            MapControllerRoute(
                endpoints,
                EzGetRoutesName.PackageContent.GetPackageContent,
                $"{ServiceIndexUrlConsts.PackageBaseAddressUrl}/{{id}}/{{version}}/{{idDotVersion}}.nuspec",
                nameof(PackageContentApiController),
                nameof(PackageContentApiController.GetPackageManifestAsync),
                "GET");
        }

        private void MapPackagePublishRoutes(IEndpointRouteBuilder endpoints)
        {
            MapControllerRoute(
                endpoints,
                EzGetRoutesName.PackagePublish.Create,
                ServiceIndexUrlConsts.PackagePublishUrl,
                nameof(PackagePublishApiController),
                nameof(PackagePublishApiController.CreateAsync),
                "PUT");

            MapControllerRoute(
                endpoints,
                EzGetRoutesName.PackagePublish.Unlist,
                $"{ServiceIndexUrlConsts.PackagePublishUrl}/{{id}}/{{version}}",
                nameof(PackagePublishApiController),
                nameof(PackagePublishApiController.UnlistAsync),
                "DELETE");

            MapControllerRoute(
                endpoints,
                EzGetRoutesName.PackagePublish.Relist,
                $"{ServiceIndexUrlConsts.PackagePublishUrl}/{{id}}/{{version}}",
                nameof(PackagePublishApiController),
                nameof(PackagePublishApiController.RelistAsync),
                "POST");
        }

        private void MapControllerRoute(
            IEndpointRouteBuilder endpoints,
            string name,
            string pattern,
            string controller,
            string action,
            string httpMethod)
        {
            var controllerName = GetControllerName(controller);
            var actionName = GetActionName(action);
            var optionsValue = Options.Value;
            var parrentName = string.Format(optionsValue.FeedPatternFormat, "{" + optionsValue.FeedRouteName + "}");

            endpoints.MapControllerRoute(
                name: name,
                pattern: pattern,
                defaults: new
                {
                    controller = controllerName,
                    action = actionName
                },
                constraints: new { httpMethod = new HttpMethodRouteConstraint(httpMethod) });

            endpoints.MapControllerRoute(
                name: EzGetRoutesName.Feed + name,
                pattern: $"{parrentName}/{pattern}",
                defaults: new
                {
                    controller = controllerName,
                    action = actionName
                },
                constraints: new { httpMethod = new HttpMethodRouteConstraint(httpMethod) });
        }

        private static string GetControllerName(string controllerName)
        {
            if (!controllerName.EndsWith(_controllerSuffix))
                return controllerName;

            var index = controllerName.LastIndexOf(_controllerSuffix);

            if (index < 0)
            {
                return controllerName;
            }

            return controllerName[..controllerName.LastIndexOf(_controllerSuffix)];
        }

        private static string GetActionName(string actionName)
        {
            if (!actionName.EndsWith(_asyncSuffix))
                return actionName;

            var index = actionName.LastIndexOf(_asyncSuffix);

            if (index < 0)
            {
                return actionName;
            }

            return actionName[..actionName.LastIndexOf(_asyncSuffix)];
        }
    }
}
