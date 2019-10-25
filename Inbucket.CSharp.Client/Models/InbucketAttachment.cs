namespace Inbucket.CSharp.Client
{
    public class InbucketAttachment
    {
        public string Filename { get; set; }

        public string ContentType { get; set; }

        public string DownloadLink { get; set; }

        public string ViewLink { get; set; }

        public string Md5 { get; set; }
    }
}
