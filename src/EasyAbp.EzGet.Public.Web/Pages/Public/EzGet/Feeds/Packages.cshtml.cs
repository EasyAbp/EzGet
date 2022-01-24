using EasyAbp.EzGet.Public.Feeds;
using EasyAbp.EzGet.Public.NuGet.Packages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EasyAbp.EzGet.Public.Web.Pages.Public.EzGet.Feeds
{
    public class PackagesModel : EzGetPublicPageModel
    {
        [FromRoute]
        public string FeedName { get; set; }

        protected IFeedPublicAppService FeedPublicAppService { get; }
        protected INuGetPackagePublicAppService NuGetPackagePublicAppService { get; }

        public PackagesModel(
            IFeedPublicAppService feedPublicAppService,
            INuGetPackagePublicAppService nuGetPackagePublicAppService)
        {
            FeedPublicAppService = feedPublicAppService;
            NuGetPackagePublicAppService = nuGetPackagePublicAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            var feed = await FeedPublicAppService.FindByNameAsync(FeedName);

            if (null == feed)
            {
                return NotFound();
            }

            //await NuGetPackagePublicAppService.GetAsync

            return Page();
        }
    }
}
