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
        /// <param name="token">GitterProvider auth token</param>
        /// <param name="text">Text of the message to send.
        /// <returns>Returns success/error/timestamp <see cref="GitterChatMessageResult"/></returns>
        [CakeAliasCategory("Chat")]
        public GitterChatMessageResult PostMessage(
            string token,
            string message)
        {
            return _context.PostMessage(
                message,
                new GitterChatMessageSettings { Token = token });
        }

        /// <summary>
        /// Post message to Gitter Room
        /// </summary>
        /// <param name="text">Text of the message to send.
        /// <returns>Returns success/error/timestamp <see cref="GitterChatMessageResult"/></returns>
        /// <param name="messageSettings">Lets you override default settings like UserName, IconUrl or if it should ThrowOnFail</param>
        [CakeAliasCategory("Chat")]
        public GitterChatMessageResult PostMessage(
            string message,
            GitterChatMessageSettings messageSettings)
        {
            if (messageSettings == null)
            {
                throw new ArgumentNullException("messageSettings");
            }

            return _context.PostMessage(
                message,
                messageSettings);
        }
    }
}