using System;
using System.Linq;

namespace Models.Helpers
{
    public static class UrlValidatorHelper
    {
        private static readonly string[] _acceptedUriSchemes = new string[] { Uri.UriSchemeHttp, Uri.UriSchemeHttps };

        public static bool UrlStringIsValid(string urlString)
        {
            var isValid = Uri.TryCreate(urlString, UriKind.Absolute, out var uriResult)
                            && _acceptedUriSchemes.Any(x => x == uriResult.Scheme);

            return isValid;
        }
    }
}
