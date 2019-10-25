namespace Inbucket.CSharp.Client
{
    using System.Collections.Generic;

    public class InbucketMessage : InbucketMessageDetails
    {
        public InbucketBody Body { get; set; }

        public Dictionary<string, List<string>> Header { get; set; }

        public List<InbucketAttachment> Attachments { get; set; }
    }
}
