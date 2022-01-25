using EasyAbp.EzGet.Public.Feeds;
using EasyAbp.EzGet.Public.NuGet.Packages;
using EasyAbp.EzGet.Public.PackageRegistrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;

namespace EasyAbp.EzGet.Public.Web.Pages.Public.EzGet.Feeds
{
    public class PackagesModel : EzGetPublicPageModel
    {
        public const int PageSize = 10;

        [FromRoute]
        public string FeedName { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public PagedResultDto<PackageRegistrationDto> PackageRegistrations { get; set; }

        public PagerModel PagerModel => new PagerModel(PackageRegistrations.TotalCount, PackageRegistrations.Items.Count, CurrentPage, PageSize, Request.Path.ToString());

        protected IFeedPublicAppService FeedPublicAppService { get; }
        protected IPackageRegistrationPublicAppService PackageRegistrationPublicAppService { get; }

        public PackagesModel(
            IFeedPublicAppService feedPublicAppService,
            IPackageRegistrationPublicAppService packageRegistrationPublicAppService)
        {
            FeedPublicAppService = feedPublicAppService;
            PackageRegistrationPublicAppService = packageRegistrationPublicAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync(int pageIndex, string filter)
        {
            var feed = await FeedPublicAppService.FindByNameAsync(FeedName);

            if (null == feed)
            {
                return NotFound();
            }

            PackageRegistrations = await PackageRegistrationPublicAppService.GetListAsync(new GetPackageRegistrationsInput
            {
                MaxResultCount = 10,
                SkipCount = pageIndex * 10,
                FeedId = feed.Id,
                Filter = filter
            });

            return Page();
        }
    }
}
