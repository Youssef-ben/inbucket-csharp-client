﻿namespace Inbucket.CSharp.Client
{
    using System.Collections.Generic;

    public class InbucketMessageHeader
    {
        public string Mailbox { get; set; }

        public string Id { get; set; }

        public string From { get; set; }

        public List<string> To { get; set; }

        public string Date { get; set; }

        public string Subject { get; set; }

        public int Size { get; set; }
    }
}
