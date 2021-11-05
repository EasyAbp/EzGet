using EasyAbp.EzGet.NuGet.ServiceIndexs;
using EasyAbp.EzGet.Public.NuGet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using System;

namespace EasyAbp.EzGet.Public
{
    public static class EzGetEndpointBuilderExtensions
    {
        private const string _feedPattern = "F/{feed}";
        private const string _asyncSuffix = "Async";
        private const string _controllerSuffix = "Controller";

        public static void MapEzGetEndpoints(this IEndpointRouteBuilder endpoints)
        {
            MapPackagePublishRoutes(endpoints);
        }

        private static void MapPackagePublishRoutes(IEndpointRouteBuilder endpoints)
        {
            //non feed
            endpoints.MapControllerRoute(
                name: EzGetRoutesName.PackagePublish.Create,
                pattern: $"{ServiceIndexUrlConsts.PackagePublishUrl}",
                defaults: new
                {
                    controller = GetControllerName(nameof(PackagePublishApiController)),
                    action = GetActionName(nameof(PackagePublishApiController.CreateAsync))
                },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("PUT") });

            endpoints.MapControllerRoute(
                name: EzGetRoutesName.PackagePublish.Unlist,
                pattern: $"{ServiceIndexUrlConsts.PackagePublishUrl}/{{id}}/{{version}}",
                defaults: new
                {
                    controller = GetControllerName(nameof(PackagePublishApiController)),
                    action = GetActionName(nameof(PackagePublishApiController.UnlistAsync))
                },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("DELETE") });

            endpoints.MapControllerRoute(
                name: EzGetRoutesName.PackagePublish.Relist,
                pattern: $"{ServiceIndexUrlConsts.PackagePublishUrl}/{{id}}/{{version}}",
                defaults: new
                {
                    controller = GetControllerName(nameof(PackagePublishApiController)),
                    action = GetActionName(nameof(PackagePublishApiController.RelistAsync))
                },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") });

            //feed
            endpoints.MapControllerRoute(
                name: EzGetRoutesName.Feed + EzGetRoutesName.PackagePublish.Create,
                pattern: $"{_feedPattern}/{ServiceIndexUrlConsts.PackagePublishUrl}",
                defaults: new
                {
                    controller = GetControllerName(nameof(PackagePublishApiController)),
                    action = GetActionName(nameof(PackagePublishApiController.CreateAsync))
                },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("PUT") });

            endpoints.MapControllerRoute(
                name: EzGetRoutesName.Feed + EzGetRoutesName.PackagePublish.Unlist,
                pattern: $"{_feedPattern}/{ServiceIndexUrlConsts.PackagePublishUrl}/{{id}}/{{version}}",
                defaults: new 
                {
                    controller = GetControllerName(nameof(PackagePublishApiController)),
                    action = GetActionName(nameof(PackagePublishApiController.UnlistAsync))
                },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("DELETE") });

            endpoints.MapControllerRoute(
                name: EzGetRoutesName.Feed + EzGetRoutesName.PackagePublish.Relist,
                pattern: $"{ServiceIndexUrlConsts.PackagePublishUrl}/{{id}}/{{version}}",
                defaults: new
                {
                    controller = GetControllerName(nameof(PackagePublishApiController)),
                    action = GetActionName(nameof(PackagePublishApiController.RelistAsync))
                },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") });
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
