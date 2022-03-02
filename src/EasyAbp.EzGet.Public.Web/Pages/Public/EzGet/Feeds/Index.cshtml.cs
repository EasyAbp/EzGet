using System.Threading.Tasks;
using EasyAbp.EzGet.Feeds;
using EasyAbp.EzGet.Public.Feeds;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;

namespace EasyAbp.EzGet.Public.Web.Pages.Public.EzGet.Feeds
{
    public class IndexModel : EzGetPublicPageModel
    {
        public const int PageSize = 10;
        
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public PagedResultDto<FeedDto> Feeds { get; set; }
        
        public PagerModel PagerModel => new PagerModel(Feeds.TotalCount, Feeds.Items.Count, CurrentPage, PageSize, Request.Path.ToString());

        protected IFeedPublicAppService FeedPublicAppService { get; }

        public IndexModel(IFeedPublicAppService feedPublicAppService)
        {
            FeedPublicAppService = feedPublicAppService;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            Feeds = await FeedPublicAppService.GetListAsync(new GetFeedsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize
            });

            return Page();
        }
    }
}
