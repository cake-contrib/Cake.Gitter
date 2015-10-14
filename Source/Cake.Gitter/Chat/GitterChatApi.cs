using System;
using System.Linq;
using System.Net;
using System.Text;
using Cake.Common.Diagnostics;
using Cake.Core;

namespace Cake.Gitter.Chat
{
    internal static class GitterChatApi
    {
        const string PostMessageUri = "https://api.gitter.im/v1/rooms/{0}/chatMessages";

        internal static GitterChatMessageResult PostMessage(
            this ICakeContext context,
            string message,
            GitterChatMessageSettings messageSettings)
        {
            if (messageSettings == null)
            {
                throw new ArgumentNullException("messageSettings", "Invalid gitter message specified");
            }

            GitterChatMessageResult result;
            if (!string.IsNullOrWhiteSpace(messageSettings.IncomingWebHookUrl))
            {
                result = PostToIncomingWebHook(
                    context,
                    message,
                    messageSettings);
            }
            else
            {
                result = context.PostToChatApi(
                    PostMessageUri,
                    message,
                    messageSettings);
            }

            if (!result.Ok && messageSettings.ThrowOnFail == true)
            {
                throw new CakeException(result.Error ?? "Failed to send message, unknown error");
            }

            return result;
        }

        private static GitterChatMessageResult PostToIncomingWebHook(
            ICakeContext context,
            string message,
            GitterChatMessageSettings messageSettings)
        {
            if (messageSettings == null)
            {
                throw new ArgumentNullException("messageSettings", "Invalid gitter message specified");
            }

            if (string.IsNullOrWhiteSpace(messageSettings.IncomingWebHookUrl))
            {
                throw new NullReferenceException("Invalid IncomingWebHookUrl supplied.");
            }

            context.Verbose(
                "Posting to incoming webhook {0}...",
                string.Concat(messageSettings.IncomingWebHookUrl.TrimEnd('/').Reverse().SkipWhile(c => c != '/').Reverse()));

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                var postBytes = Encoding.UTF8.GetBytes(string.Format("message={0}", message));

                var resultBytes = client.UploadData(
                        messageSettings.IncomingWebHookUrl,
                        "POST",
                        postBytes);

                var result = Encoding.UTF8.GetString(resultBytes);

                var parsedResult = new GitterChatMessageResult(
                    StringComparer.OrdinalIgnoreCase.Equals(result, "ok"),
                    string.Empty,
                    StringComparer.OrdinalIgnoreCase.Equals(result, "ok") ? string.Empty : result);

                context.Debug("Result parsed: {0}", parsedResult);

                return parsedResult;
            }
        }

        private static GitterChatMessageResult PostToChatApi(
            this ICakeContext context,
            string templateRoomUri,
            string message,
            GitterChatMessageSettings messageSettings)
        {
            var roomUri = string.Format(templateRoomUri, messageSettings.RoomId);

            context.Verbose("Posting to {0}", roomUri);

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = string.Format("bearer {0}", messageSettings.Token);

                var jsonPayload = Encoding.UTF8.GetBytes(string.Format("{{\"text\": \"{0}\"}}", message));

                var resultBytes = client.UploadData(roomUri, jsonPayload);
                var resultJson = Encoding.UTF8.GetString(resultBytes);

                context.Debug("Result json: {0}", resultJson);

                return null;
            }
        }
    }
}