namespace Inbucket.CSharp.Client
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class InbucketMessageSource
    {
        public string Date { get; set; }

        public List<string> To { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        [JsonProperty("x-mailer")]
        public string XMailer { get; set; }
    }
}
