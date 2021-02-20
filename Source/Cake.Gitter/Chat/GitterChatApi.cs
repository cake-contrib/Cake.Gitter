using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Gitter.LitJson;

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
        [CakeMethodAlias]
        internal static GitterChatMessageResult PostMessage(this ICakeContext context, string message, GitterChatMessageSettings messageSettings)
        {
            GitterChatMessageResult result;
            if (!string.IsNullOrWhiteSpace(messageSettings.IncomingWebHookUrl))
            {
                result = PostToIncomingWebHook(context, message, messageSettings).Result;
            }
            else
            {
                result = context.PostToChatApi(message, messageSettings).Result;
            }

            if (!result.Ok && messageSettings.ThrowOnFail == true)
            {
                throw new CakeException(result.Error ?? "Failed to send message, unknown error");
            }

            return result;
        }

        private static async Task<GitterChatMessageResult> PostToIncomingWebHook(ICakeContext context, string message, GitterChatMessageSettings messageSettings)
        {
            context.Verbose("Posting to incoming webhook {0}...", string.Concat(messageSettings.IncomingWebHookUrl.TrimEnd('/').Reverse().SkipWhile(c => c != '/').Reverse()));

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"message", message},
                    {"level", messageSettings.MessageLevel == GitterMessageLevel.Error ? "error" : "info"}
                });

            var httpResponse = await httpClient.PostAsync(new Uri(messageSettings.IncomingWebHookUrl), content);
            var response = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonMapper.ToObject(response);

            var parsedResult = new GitterChatMessageResult(
                httpResponse.IsSuccessStatusCode,
                DateTime.UtcNow.ToString("u"),
                string.Empty);

            context.Debug("Result parsed: {0}", parsedResult);

            return parsedResult;
        }

        [CakeMethodAlias]
        private static async Task<GitterChatMessageResult> PostToChatApi(this ICakeContext context, string message, GitterChatMessageSettings messageSettings)
        {
            if (string.IsNullOrWhiteSpace(messageSettings.Token))
            {
                throw new NullReferenceException("No authorization token provided.");
            }

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrWhiteSpace(messageSettings.Token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", messageSettings.Token);
            }

            string url = $"https://api.gitter.im/v1/rooms/{messageSettings.RoomId}/chatMessages";

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"text", message}
            });

            var httpResponse = await httpClient.PostAsync(url, content);
            var response = await httpResponse.Content.ReadAsStringAsync();

            var result = JsonMapper.ToObject(response);
            var parsedResult = new GitterChatMessageResult(
                httpResponse.IsSuccessStatusCode,
                result.GetString("sent"),
                string.Empty);

            context.Debug("Result parsed: {0}", parsedResult);

            return parsedResult;
        }

        private static string GetString(this JsonData data, string key)
        {
            return (data != null && data.Keys.Contains(key))
                ? (string)data[key]
                : null;
        }
    }
}
