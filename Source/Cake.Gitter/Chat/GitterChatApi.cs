using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using Cake.Common.Diagnostics;
using Cake.Core;

namespace Cake.Gitter.Chat
{
    internal static class GitterChatApi
    {
        const string PostMessageUri = "https://slack.com/api/chat.postMessage";

        internal static GitterChatMessageResult PostMessage(
            this ICakeContext context,
            string text,
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
                    text,
                    messageSettings);
            }
            else
            {
                var messageParams = GetMessageParams(
                    messageSettings.Token,
                    text);

                result = context.PostToChatApi(
                    PostMessageUri,
                    messageParams);
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

            ////var postJson = JsonMapper.ToJson(
            ////    new
            ////    {
            ////        message
            ////    });

            ////context.Debug("Parameter: {0}", postJson);

            using (var client = new WebClient())
            {
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
            string apiUri,
            NameValueCollection apiParameters)
        {
            ////using (var client = new WebClient())
            ////{
            ////    context.Verbose("Posting to {0}", apiUri);

            ////    context.Verbose(
            ////        "Parematers: {0}",
            ////        apiParameters
            ////            .Keys
            ////            .Cast<string>()
            ////            .Aggregate(
            ////                new StringBuilder(),
            ////                (sb, key) =>
            ////                {
            ////                    sb.AppendFormat(
            ////                        "{0}={1}\r\n",
            ////                        key,
            ////                        (StringComparer.InvariantCultureIgnoreCase.Equals(key, "token"))
            ////                            ? "*redacted*"
            ////                            : string.Join(
            ////                                ",",
            ////                                apiParameters.GetValues(key) ?? new string[0]));
            ////                    return sb;
            ////                },
            ////                r => r.ToString()));

            ////    var resultBytes = client.UploadValues(apiUri, apiParameters);
            ////    var resultJson = Encoding.UTF8.GetString(resultBytes);

            ////    context.Debug("Result json: {0}", resultJson);

            ////    var result = JsonMapper.ToObject(resultJson);
            ////    var parsedResult = new GitterChatMessageResult(
            ////        result.GetBoolean("ok") ?? false,
            ////        result.GetString("channel"),
            ////        result.GetString("ts"),
            ////        result.GetString("error"));

            ////    context.Debug("Result parsed: {0}", parsedResult);

            ////    return parsedResult;

            return null;
        }

        private static NameValueCollection GetMessageParams(
            string token,
            string message)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException("token", "Invalid Message Token specified");
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException("message", "Invalid Message Text specified");
            }

            var messageParams = new NameValueCollection
            {
                { "token", token },
                { "message", message }
            };

            return messageParams;
        }
    }
}