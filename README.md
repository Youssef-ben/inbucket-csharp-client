# Inbucket CSharp Client

[![Build Status](https://travis-ci.com/Youssef-ben/inbucket-csharp-client.svg?token=1yAYS99Fximr1SbNeiB9&branch=develop)](https://travis-ci.com/Youssef-ben/inbucket-csharp-client)

CSharp (.NET Core) client for the Inbucket API.

## Inbucket

[Inbucket](https://github.com/inbucket/inbucket) is an email testing service, it will accept messages for any email address and make them available via web, REST and POP3. Read more at the [Inbucket Website](https://www.inbucket.org/).

## About the library

### REST API

The current library support all the base methods offered by [Inbucket](https://github.com/inbucket/inbucket/wiki/REST-API):

- List Mailbox Contents.
- Get Message by Id.
- Get Message source.
- Delete Message.
- Purge Mailbox.

### How to use

To start using the `Inbucket Client` you need to create a new instance. To do that, you have two choices:

```CSharp
// Create a new Instance by specifying the base Uri, the port and the MailBox.
var inbucketClient = new InbucketClient("localhost", 9000, "<mailbox-name>");

// Or, passe the URI object and the MailBox.
var inbucketClient = new InbucketClient(new Uri("http://localhost:900", "<mailbox-name>"));
```

The library offers the following methods:

```CSharp
/// <summary>
/// Get the Mailbox from the email.
/// </summary>
var mailbox = inbucketClient.GetMailBoxFromEmail("<email-address>");

/// <summary>
/// Get or Set the object {CurrentMailBox} property and returns it.
/// If you specify the MailBox, the method will set the object {CurrentMailBox} property and return the values.
/// If nothing specifid, the method will simply return the current MailBox value.
/// </summary>
var mailbox = inbucketClient.MailBox("<email-address>");

/// <summary>
/// Gets the specified message details.
/// Calls the Inbucket endpoint [GET] {/api/v1/mailbox/{mailbox}/{id}}.
/// Note: if the {mailbox} parameter is not specified the method will take the object default mailbox.
/// If Specified, it will set the object default mailbox before calling the Inbucket API.
/// </summary>
var message = await inbucketClient.GetMessageAsync("<message-id>", (optional)"<mailbox-name>");

/// <summary>
/// Gets the specified message source information.
/// Calls the Inbucket endpoint [GET] {/api/v1/mailbox/{mailbox}/{id}/source}.
/// Note: if the {mailbox} parameter is not specified the method will take the object default mailbox.
/// If Specified, it will set the object default mailbox before calling the Inbucket API.
/// </summary>
var messageSource = await inbucketClient.GetMessageSourceAsync("<message-id>", (optional)"<mailbox-name>");

/// <summary>
/// Gets All the messages basic details from the specified MailBox.
/// Calls the Inbucket endpoint [GET] {/api/v1/mailbox/{mailbox}.
/// Note: if the {mailbox} parameter is not specified the method will take the object default mailbox.
/// If Specified, it will set the object default mailbox before calling the Inbucket API.
/// </summary>
var mailboxContent = await inbucketClient.GetMailBoxMessagesAsync((optional)"<mailbox-name>");

/// <summary>
/// Delete the message from the mailbox.
/// Calls the Inbucket endpoint [DELETE] {/api/v1/mailbox/{mailbox}/{id}}.
/// Note: if the {mailbox} parameter is not specified the method will take the object default mailbox.
/// If Specified, it will set the object default mailbox before calling the Inbucket API.
/// </summary>
var isDeleted = await inbucketClient.DeleteMessageAsync(string id, string mailbox = default);

/// <summary>
/// Delete all the messages from the mailbox.
/// Calls the Inbucket endpoint [DELETE] {/api/v1/mailbox/{mailbox}}.
/// Note: if the {mailbox} parameter is not specified the method will take the object default mailbox.
/// If Specified, it will set the object default mailbox before calling the Inbucket API.
/// </summary>
var isPurged = await inbucketClient.PurgeMailBoxAsync(string mailbox = default);
```
