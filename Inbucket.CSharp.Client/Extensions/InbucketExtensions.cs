namespace Inbucket.CSharp.Client.Extensions
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public static class InbucketExtensions
    {
        public static HttpClient GetHttpClient(string host, int port = 9000)
        {
            return new HttpClient
            {
                BaseAddress = new Uri($"http://{host}:{port}"),
            };
        }

        /// <summary>
        /// Checks if the specified string is a valid Email address.
        /// source: https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        /// </summary>
        /// <param name="self">The the Inbucket Client class.</param>
        /// <param name="email">The email to be validated.</param>
        /// <returns>True if the email is valid. False otherwise.</returns>
        internal static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Reads the the reponse content and convert it to Targeted object.
        /// </summary>
        /// <typeparam name="T">The target object.</typeparam>
        /// <param name="response">The Inbucket API response.</param>
        /// <returns>The Inbucket response as an object.</returns>
        internal static async Task<T> ReadAsJsonAsync<T>(this HttpResponseMessage response)
        {
            var dataAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(dataAsString);
        }

        /// <summary>
        /// Use the [GET] request to Inbucket API and return the targeted object. 
        /// </summary>
        /// <typeparam name="T">The targeted object.</typeparam>
        /// <param name="self">The instance of the Inbucket client.</param>
        /// <param name="endpoint">The Inbucket API endpoint.</param>
        /// <returns>The targeted results.</returns>
        internal static async Task<T> GetAsync<T>(this InbucketClient self, string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new ArgumentNullException("API EndPoint required.");
            }

            var response = await self.Client.GetAsync(endpoint);

            return await response.ReadAsJsonAsync<T>();
        }

        /// <summary>
        /// Use the [DELETE] request to Inbucket API and return the execution status. 
        /// </summary>
        /// <param name="self">The instance of the Inbucket client.</param>
        /// <param name="endpoint">The Inbucket API endpoint.</param>
        /// <returns>True if deleted correctly, False otherwise.</returns>
        internal static async Task<bool> DeleteAsync(this InbucketClient self, string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new ArgumentNullException("API EndPoint required.");
            }

            var response = await self.Client.DeleteAsync(endpoint);

            return response.IsSuccessStatusCode;
        }
    }
}
