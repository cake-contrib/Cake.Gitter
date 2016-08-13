using Cake.Core;
using Cake.Gitter.Chat;

namespace Cake.Gitter
{
    /// <summary>
    /// Contains functionality related to Gitter API
    /// </summary>
    public sealed class GitterProvider
    {
        private readonly GitterChatProvider _chat;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitterProvider"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GitterProvider(ICakeContext context)
        {
            _chat = new GitterChatProvider(context);
        }

        /// <summary>
        /// Gets the Gitter Chat functionality.
        /// </summary>
        public GitterChatProvider Chat
        {
            get
            {
                return _chat;
            }
        }
    }
}