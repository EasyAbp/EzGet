using EasyAbp.EzGet.Feeds;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Volo.Abp.Authorization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace EasyAbp.EzGet.Public
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IMethodInvocationAuthorizationService))]
    public class EzGetMethodInvocationAuthorizationService : MethodInvocationAuthorizationService, ITransientDependency
    {
        protected IFeedStore FeedStore { get; }
        protected IHttpContextAccessor HttpContextAccessor { get; }

        public EzGetMethodInvocationAuthorizationService(
            IAbpAuthorizationPolicyProvider abpAuthorizationPolicyProvider,
            IAbpAuthorizationService abpAuthorizationService,
            IFeedStore feedStore,
            IHttpContextAccessor httpContextAccessor)
            : base(abpAuthorizationPolicyProvider, abpAuthorizationService)
        {
            FeedStore = feedStore;
            HttpContextAccessor = httpContextAccessor;
        }

        protected override bool AllowAnonymous(MethodInvocationAuthorizationContext context)
        {
            if (base.AllowAnonymous(context))
            {
                return true;
            }

            if (!context.Method.GetCustomAttributes(true).OfType<AllowAnonymousIfFeedPublicAttribute>().Any())
            {
                return false;
            }

            if (!HttpContextAccessor.HttpContext.Request.RouteValues.TryGetValue(EzGetPublicHttpApiNuGetConsts.FeedRouteName, out var feedName) || null == feedName)
            {
                return false;
            }

            var feed = AsyncHelper.RunSync(() => FeedStore.GetAsync(feedName.ToString()));

            if (null == feed)
            {
                return false;
            }

            return null == feed ? false : feed.FeedType == FeedTypeEnum.Public;
        }
    }
}
