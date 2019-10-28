using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Inbucket.CSharp.Client
{
    public interface IInbucketClient
    {
        HttpClient Client { get; set; }

        /// <summary>
        /// <para>Get the Mailbox from the specified email.</para>
        /// <para>The Method first checks that the given email is a valid one then returns the MailBox.</para>
        /// If the {setMailBox} is true, the method will set the object property {CurrentMailBox} and then returns the Mailbox.
        /// </summary>
        /// <param name="email">The email address from which we need to extract the MailBox.</param>
        /// <param name="setMailBox">Determine if we should set the MailBox of the current instance.</param>
        /// <returns>MailBox.</returns>
        /// <exception cref="FormatException">Thrown when the Email address is invalid.</exception>
        string GetMailBoxFromEmail(string email, bool setMailBox = true);

        /// <summary>
        /// Get or Set the object {CurrentMailBox} property and returns it.
        /// <para>If you specify the MailBox, the method will set the object {CurrentMailBox} property and return the values.
        /// If nothing specifid, the method will simply return the current MailBox value.</para>
        /// </summary>
        /// <param name="mailbox">The desired MailBox.</param>
        /// <returns>The object current mailbox.</returns>
        string MailBox(string mailbox = default);

        /// <summary>
        /// <para>Gets the specified message details.</para>
        /// <para>Calls the Inbucket endpoint [GET] {/api/v1/mailbox/{mailbox}/{id}}.</para>
        /// Note: if the {mailbox} parameter is not specified the method will take the object default mailbox.
        /// If Specified, it will set the object default mailbox before calling the Inbucket API.
        /// </summary>
        /// <param name="id">The message ID.</param>
        /// <param name="mailbox">(Optional) The Inbucket MailBox from which we should look for the message.</param>
        /// <returns>The Inbucket message details.</returns>
        Task<InbucketMessage> GetMessageAsync(string id, string mailbox = default);

        /// <summary>
        /// <para>Gets the specified message source information.</para>
        /// <para>Calls the Inbucket endpoint [GET] {/api/v1/mailbox/{mailbox}/{id}/source}.</para>
        /// Note: if the {mailbox} parameter is not specified the method will take the object default mailbox.
        /// If Specified, it will set the object default mailbox before calling the Inbucket API.
        /// </summary>
        /// <param name="id">The message ID.</param>
        /// <param name="mailbox">(Optional) The Inbucket MailBox from which we should look for the message.</param>
        /// <returns>The Inbucket message details.</returns>
        Task<InbucketMessageSource> GetMessageSourceAsync(string id, string mailbox = default);

        /// <summary>
        /// <para>Gets All the messages basic details from the specified MailBox.</para>
        /// <para>Calls the Inbucket endpoint [GET] {/api/v1/mailbox/{mailbox}.</para>
        /// Note: if the {mailbox} parameter is not specified the method will take the object default mailbox.
        /// If Specified, it will set the object default mailbox before calling the Inbucket API.
        /// </summary>
        /// <param name="mailbox">(Optional) The Inbucket MailBox from which we should look for the messages.</param>
        /// <returns>The list of the Inbucket basic message details.</returns>
        Task<List<InbucketMessageHeader>> GetMailBoxMessagesAsync(string mailbox = default);

        /// <summary>
        /// <para>Delete the message from the mailbox.</para>
        /// <para>Calls the Inbucket endpoint [DELETE] {/api/v1/mailbox/{mailbox}/{id}}.</para>
        /// Note: if the {mailbox} parameter is not specified the method will take the object default mailbox.
        /// If Specified, it will set the object default mailbox before calling the Inbucket API.
        /// </summary>
        /// <param name="id">The message ID.</param>
        /// <param name="mailbox">(Optional) The Inbucket MailBox from which we should look for the message.</param>
        /// <returns>True if deleted, False otherwise.</returns>
        Task<bool> DeleteMessageAsync(string id, string mailbox = default);

        /// <summary>
        /// <para>Delete all the messages from the mailbox.</para>
        /// <para>Calls the Inbucket endpoint [DELETE] {/api/v1/mailbox/{mailbox}}.</para>
        /// Note: if the {mailbox} parameter is not specified the method will take the object default mailbox.
        /// If Specified, it will set the object default mailbox before calling the Inbucket API.
        /// </summary>
        /// <param name="mailbox">(Optional) The Inbucket MailBox from which we should look for the message.</param>
        /// <returns>True if deleted, False otherwise.</returns>
        Task<bool> PurgeMailBoxAsync(string mailbox = default);
    }
}
