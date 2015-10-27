using System;
using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Core;
using GitterSharp.Model;
using GitterSharp.Services;

namespace Cake.Gitter.Chat
{
    /// <summary>
    /// The actual worker for posting messages to Gitter
    /// </summary>
    internal static class GitterChatApi
    {
        /// <summary>
        /// Sends a message to Gitter, based on the provided settings
        /// </summary>
        /// <param name="context">The Cake Context</param>
        /// <param name="message">The message to be sent</param>
        /// <param name="messageSettings">The settings to be used when sending the message</param>
        /// <returns>An instance of <see cref="GitterChatMessageResult"/> indicating success/failure</returns>
        internal static GitterChatMessageResult PostMessage(this ICakeContext context, string message, GitterChatMessageSettings messageSettings)
        {
            GitterChatMessageResult result;
            if (!string.IsNullOrWhiteSpace(messageSettings.IncomingWebHookUrl))
            {
                result = PostToIncomingWebHook(context, message, messageSettings);
            }
            else
            {
                result = context.PostToChatApi(message, messageSettings);
            }

            if (!result.Ok && messageSettings.ThrowOnFail == true)
            {
                throw new CakeException(result.Error ?? "Failed to send message, unknown error");
            }

            return result;
        }

        private static GitterChatMessageResult PostToIncomingWebHook(ICakeContext context, string message, GitterChatMessageSettings messageSettings)
        {
            context.Verbose("Posting to incoming webhook {0}...", string.Concat(messageSettings.IncomingWebHookUrl.TrimEnd('/').Reverse().SkipWhile(c => c != '/').Reverse()));

            var gitterWebHookService = new WebhookService();
            var result = gitterWebHookService.PostAsync(messageSettings.IncomingWebHookUrl, message);

            var parsedResult = new GitterChatMessageResult(result.Result, DateTime.UtcNow.ToString("u"), string.Empty);

            context.Debug("Result parsed: {0}", parsedResult);

            return parsedResult;
        }

        private static GitterChatMessageResult PostToChatApi(this ICakeContext context, string message, GitterChatMessageSettings messageSettings)
        {
            if (string.IsNullOrWhiteSpace(messageSettings.Token))
            {
                throw new NullReferenceException("No authorization token provided.");
            }

            var gitterApiService = new GitterApiService(messageSettings.Token);
            var messageResponse = gitterApiService.SendMessageAsync(messageSettings.RoomId, message);

            var parsedResult = new GitterChatMessageResult(!string.IsNullOrWhiteSpace(messageResponse.Result.Id), messageResponse.Result.SentDate.ToString("u"), string.Empty);

            context.Debug("Result parsed: {0}", parsedResult);

            return parsedResult;
        }
    }
}