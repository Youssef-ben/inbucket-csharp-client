namespace Inbucket.CSharp.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Inbucket.CSharp.Client.Extensions;

    public class InbucketClient : IInbucketClient
    {
        private const string V1_MESSAGE_ENDPOINT = "/api/v1/mailbox/{0}/{1}";
        private const string V1_MESSAGE_SOURCE_ENDPOINT = "/api/v1/mailbox/{0}/{1}/source";
        private const string V1_MAILBOX_ENDPOINT = "/api/v1/mailbox/{0}";

        public readonly HttpClient Client;

        private string CurrentMailBox { get; set; }

        public InbucketClient(Uri inbucketUri, string mailbox = default)
        {
            if (!string.IsNullOrWhiteSpace(mailbox))
            {
                this.CurrentMailBox = mailbox;
            }

            this.Client = new HttpClient
            {
                BaseAddress = inbucketUri,
            };
        }

        public InbucketClient(string baseUri, int port = 9000, string mailbox = default, bool useHttps = false)
        {
            if (!string.IsNullOrWhiteSpace(mailbox))
            {
                this.CurrentMailBox = mailbox;
            }

            var uri = $"{baseUri}:{port}";
            var protocol = useHttps ? "https" : "http";

            this.Client = new HttpClient
            {
                BaseAddress = new Uri($"{protocol}://{uri}"),
            };
        }

        public string GetMailBoxFromEmail(string email, bool setMailBox = true)
        {
            if (InbucketExtensions.IsValidEmail(email))
            {
                throw new FormatException("Invalid email address.");
            }

            var mailbox = email.Split("@")[0];

            if (!setMailBox)
            {
                return mailbox;
            }

            return this.CurrentMailBox = mailbox;
        }

        public string MailBox(string mailbox = default)
        {
            if (!string.IsNullOrWhiteSpace(mailbox))
            {
                this.CurrentMailBox = mailbox;
            }

            return this.CurrentMailBox;
        }

        public async Task<InbucketMessage> GetMessageAsync(string id, string mailbox = default)
        {
            mailbox = this.MailBox(mailbox);

            var endpoint = string.Format(V1_MESSAGE_ENDPOINT, mailbox, id);

            return await this.GetAsync<InbucketMessage>(endpoint);
        }

        public async Task<InbucketMessageSource> GetMessageSource(string id, string mailbox = default)
        {
            mailbox = this.MailBox(mailbox);

            var endpoint = string.Format(V1_MESSAGE_SOURCE_ENDPOINT, mailbox, id);

            return await this.GetAsync<InbucketMessageSource>(endpoint);
        }

        public async Task<List<InbucketMessageDetails>> GetMailBoxMessages(string mailbox = null)
        {
            mailbox = this.MailBox(mailbox);

            var endpoint = string.Format(V1_MAILBOX_ENDPOINT, mailbox);

            return await this.GetAsync<List<InbucketMessageDetails>>(endpoint);
        }

        public async Task<bool> DeleteMessageAsync(string id, string mailbox = null)
        {
            mailbox = this.MailBox(mailbox);

            var endpoint = string.Format(V1_MESSAGE_ENDPOINT, mailbox, id);

            return await this.DeleteAsync(endpoint);
        }

        public async Task<bool> PurgeMailBox(string mailbox = null)
        {
            mailbox = this.MailBox(mailbox);

            var endpoint = string.Format(V1_MAILBOX_ENDPOINT, mailbox);

            return await this.DeleteAsync(endpoint);
        }
    }
}
