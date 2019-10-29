namespace Inbucket.CSharp.Client.Test
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class InbucketClientTest
    {
        private readonly IInbucketClient client;

        public InbucketClientTest()
        {
            this.client = new InbucketClient("localhost");
        }

        [Fact]
        public void GetMailBoxFromEmail_Success()
        {
            var mailbox = this.client.GetMailBoxFromEmail("example@example.com");
            Assert.Equal("example", mailbox);
        }

        [Fact]
        public void GetMailBoxFromEmail_Invalid_Email()
        {
            try
            {
                var mailbox = this.client.GetMailBoxFromEmail("exampleexample.com");
            }
            catch (FormatException ex)
            {
                Assert.Contains("Invalid email address.", ex.Message);
            }
        }

        [Fact]
        public void GetMailBox_Success()
        {
            var mailbox = this.client.GetMailBoxFromEmail("example@example.com");
            Assert.Equal("example", mailbox);

            mailbox = this.client.MailBox();
            Assert.Equal("example", mailbox);

            mailbox = this.client.MailBox("unitTest");
            Assert.Equal("unitTest", mailbox);
        }

        [Fact]
        public async Task GetMessage_Success()
        {
            var message = TestExtensions.GetMessage();
            this.client.Client = TestExtensions.GetHttpClient(message);

            var result = await this.client.GetMessageAsync(message.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMessageSource_Success()
        {
            var message = TestExtensions.GetMessage();
            this.client.Client = TestExtensions.GetHttpClient(message);

            var result = await this.client.GetMessageSourceAsync(message.Id);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task GetMailBoxMessages_Success()
        {
            var message = TestExtensions.GetMailBoxContents();
            this.client.Client = TestExtensions.GetHttpClient(message);

            var result = await this.client.GetMailBoxMessagesAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteMessage_Success()
        {
            var message = TestExtensions.GetMessage();
            this.client.Client = TestExtensions.GetHttpClient(true);

            var result = await this.client.DeleteMessageAsync(message.Id);
            Assert.True(result);
        }

        [Fact]
        public async Task PurgeMailBox_Success()
        {
            var message = TestExtensions.GetMessage();
            this.client.Client = TestExtensions.GetHttpClient(true);

            var result = await this.client.DeleteMessageAsync(message.Mailbox);
            Assert.True(result);
        }
    }
}
