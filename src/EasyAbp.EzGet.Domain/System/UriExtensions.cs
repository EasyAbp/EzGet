using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class UriExtensions
    {
        public static string GetAbsoluteUriOrEmpty(this Uri uri)
        {
            if (null == uri || !uri.IsAbsoluteUri)
            {
                return string.Empty;
            }

            return uri.AbsoluteUri;
        }
    }
}
