using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Gitter.Chat
{
    /// <summary>
    /// Contains GitterProvider Chat functionality.
    /// </summary>
    [CakeAliasCategory("Gitter")]
    public sealed class GitterChatProvider
    {
        private readonly ICakeContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitterChatProvider"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GitterChatProvider(ICakeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Post message to Gitter Room
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="messageSettings">Lets you override default settings like UserName, IconUrl or if it should ThrowOnFail</param>
        /// <returns>Returns success/error/timestamp <see cref="GitterChatMessageResult"/></returns>
        /// <example>
        /// <code>
        /// Gitter.Chat.PostMessage(message:"Hello from Cake.Gitter API, messageSettings:new GitterChatMessageSettings {
        ///     Token              = "123456",
        ///     RoomId             = "123456"
        /// });
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// Gitter.Chat.PostMessage(message:"Hello from Cake.Gitter Web Hook, messageSettings:new GitterChatMessageSettings {
        ///     IncomingWebHookUrl = "http://yourwebhookurl.net"
        /// });
        /// </code>
        /// </example>
        [CakeAliasCategory("Chat")]
        public GitterChatMessageResult PostMessage(string message, GitterChatMessageSettings messageSettings)
        {
            if (messageSettings == null)
            {
                throw new ArgumentNullException("messageSettings");
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new NullReferenceException("Unable to send empty message.");
            }

            return _context.PostMessage(message, messageSettings);
        }
    }
}