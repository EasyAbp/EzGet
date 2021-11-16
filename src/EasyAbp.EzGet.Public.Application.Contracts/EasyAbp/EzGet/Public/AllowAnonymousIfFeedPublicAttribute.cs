using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EzGet.Public
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AllowAnonymousIfFeedPublicAttribute : Attribute
    {
    }
}
