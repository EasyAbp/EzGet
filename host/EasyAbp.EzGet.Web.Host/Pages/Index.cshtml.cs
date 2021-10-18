using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace EasyAbp.EzGet.Pages
{
    public class IndexModel : EzGetPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}