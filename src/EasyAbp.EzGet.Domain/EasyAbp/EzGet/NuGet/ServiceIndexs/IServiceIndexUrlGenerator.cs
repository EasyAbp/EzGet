﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.NuGet.ServiceIndexs
{
    public interface IServiceIndexUrlGenerator
    {
        /// <summary>
        /// Get the URL for the package source (also known as the "service index").
        /// See: https://docs.microsoft.com/en-us/nuget/api/service-index
        /// </summary>
        Task<string> GetServiceIndexUrlAsync();

        /// <summary>
        /// Get the URL for the root of the package content resource.
        /// See: https://docs.microsoft.com/en-us/nuget/api/package-base-address-resource
        /// </summary>
        Task<string> GetPackageBaseAddressResourceUrlAsync();

        /// <summary>
        /// Get the URL for the root of the package metadata resource.
        /// See: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource
        /// </summary>
        Task<string> GetRegistrationsBaseUrlResourceUrlAsync();

        /// <summary>
        /// See: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource#registration-index
        /// </summary>
        Task<string> GetRegistrationIndexUrlAsync(string id);

        /// <summary>
        /// See: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource#registration-leaf
        /// </summary>
        /// <returns></returns>
        Task<string> GetRegistrationLeafUrlAsync(string id, string version);

        /// <summary>
        /// Get the URL to search for packages.
        /// See: https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource
        /// </summary>
        Task<string> GetSearchQueryServiceResourceUrlAsync();

        /// <summary>
        /// Get the URL to publish packages.
        /// See: https://docs.microsoft.com/en-us/nuget/api/package-publish-resource
        /// </summary>
        Task<string> GetPackagePublishResourceUrlAsync();

        /// <summary>
        /// Download package .nupkg file
        /// See: https://docs.microsoft.com/en-us/nuget/api/package-base-address-resource#download-package-content-nupkg
        /// </summary>
        Task<string> GetPackageDownloadUrlAsync(string id, string version);

        Task<string> GetPacakgeIconUrlAsync(string id, string version);
        Task<string> GetPackageReadmeUrlAsync(string id, string version);
    }
}
