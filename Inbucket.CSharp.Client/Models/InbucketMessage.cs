namespace Inbucket.CSharp.Client
{
    using System.Collections.Generic;

    public class InbucketMessage : InbucketMessageHeader
    {
        public InbucketBody Body { get; set; }

        public Dictionary<string, List<string>> Header { get; set; }

        public List<InbucketAttachment> Attachments { get; set; }
    }
}
