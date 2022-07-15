using System.Text;
using System.Text.RegularExpressions;

namespace UnReaL.Repository
{
    public class URLValidationService : IURLValidationService
    {
        private readonly Regex _urlRegex = new(@"(http|http(s)?://)?([\w-]+\.)+[\w-]+[.com|.in|.org|.co.]+(\[\?%&=]*)?");
        public string ValidateInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) { return "Input field is empty."; }

            var errorString = new StringBuilder();

            if (!Uri.IsWellFormedUriString(input, UriKind.RelativeOrAbsolute))
            {
                errorString.Append("Invalid characters detected. ");
            }

            if (!_urlRegex.IsMatch(input))
            {
                errorString.AppendLine("Malformed URL detected. ");
            }

            if (input.Contains("//") && !input.Contains(':'))
            {
                errorString.AppendLine("Malformed :// detected. ");
            }

            if (!input.Contains("://") && !input.Contains("www"))
            {
                errorString.Append("Must have at least :// or www. ");
            }

            if (!input.Contains("://") && input.Contains("http"))
            {
                errorString.Append("Must have a :// where http(s) is present. ");
            }

            return errorString.ToString();
        }
    }
}
