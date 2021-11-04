using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EzGet.Public
{
    public class EzGetHttpApiNuGetExceptionAttribute : ExceptionFilterAttribute, ITransientDependency
    {
        protected IHttpExceptionStatusCodeFinder HttpExceptionStatusCodeFinder { get; }

        public EzGetHttpApiNuGetExceptionAttribute(
            IHttpExceptionStatusCodeFinder httpExceptionStatusCodeFinder)
        {
            HttpExceptionStatusCodeFinder = httpExceptionStatusCodeFinder;
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var httpCode = HttpExceptionStatusCodeFinder.GetStatusCode(
                context.HttpContext,
                TryToGetActualException(context.Exception));

            context.HttpContext.Response.StatusCode = (int)httpCode;
            context.Exception = null;
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }

        protected virtual Exception TryToGetActualException(Exception exception)
        {
            if (exception is AggregateException && exception.InnerException != null)
            {
                var aggException = exception as AggregateException;

                if (aggException.InnerException is AbpValidationException ||
                    aggException.InnerException is AbpAuthorizationException ||
                    aggException.InnerException is EntityNotFoundException ||
                    aggException.InnerException is IBusinessException)
                {
                    return aggException.InnerException;
                }
            }

            return exception;
        }
    }
}
