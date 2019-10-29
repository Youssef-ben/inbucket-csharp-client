namespace Inbucket.CSharp.Client.Test
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    public static class TestExtensions
    {
        /// <summary>
        /// Create and return a mock of the {HttpClient}.
        /// </summary>
        /// <typeparam name="T">Class type.</typeparam>
        /// <param name="data">Data to be returned.</param>
        /// <returns></returns>
        public static HttpClient GetHttpClient<T>(T data)
        {
            return new HttpClient(new HttpMessageHandlerMock<T>(data))
            {
                BaseAddress = new Uri("http://localhost:9000"),
            };
        }

        /// <summary>
        /// Get an instance of Inbucket Message.
        /// </summary>
        public static InbucketMessage GetMessage()
        {
            return new InbucketMessage
            {
                Id = Guid.NewGuid().ToString(),
                Mailbox = "example",
                Subject = "Unit test",
                From = "example@example.com",
                Date = DateTime.UtcNow.ToLongDateString(),
                Size = 23569,
                To = new List<string>()
                {
                    "example@example.com"
                },
                Body = new InbucketBody
                {
                    Html = "<h1>This is the body of the message.</h1>",
                    Text = "This is the body of the message.",
                },
                Header = new Dictionary<string, List<string>>
                {
                    { "Content-Type", new List<string>()
                    {
                       "multipart/alternative; boundary=\"----=_MIME_BOUNDARY_000_62717\""
                    }},
                    { "Date", new List<string>(){
                        DateTime.UtcNow.ToLongDateString()
                    }},
                    { "From", new List<string>(){
                        "example@example.com"
                    }},
                    { "Mime-Version", new List<string>(){
                        "1.0"
                    }},
                    { "Subject", new List<string>(){
                        "Swaks HTML"
                    }},
                    { "To", new List<string>(){
                        "example@example.com"
                    }},
                }
            };
        }

        /// <summary>
        /// Get a fake list of the Inbucket mailbox content.
        /// </summary>
        /// <returns></returns>
        public static List<InbucketMessageHeader> GetMailBoxContents()
        {
            return new List<InbucketMessageHeader>
            {
                TestExtensions.GetMessage(),
                TestExtensions.GetMessage(),
            };
        }
    }
}
