using Cake.Core.Annotations;

namespace Cake.Gitter.Chat
{
    /// <summary>
    /// Class that lets you override default API settings
    /// </summary>
    [CakeAliasCategory("Gitter")]
    public sealed class GitterChatMessageSettings
    {
        /// <summary>
        /// Gets or sets Gitter token used for authentication.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets Gitter Room Id.
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// Gets or sets the Incoming Web Hook Url which can be used instead of token (https://developer.gitter.im/docs/rest-api)
        /// </summary>
        public string IncomingWebHookUrl { get; set; }

        /// <summary>
        /// Gets or sets Gitter Message Level
        /// </summary>
        /// <remarks>Default is Info</remarks>
        public GitterMessageLevel MessageLevel { get; set; }

        /// <summary>
        /// Gets or sets an optional flag for if should throw exception on failure
        /// </summary>
        public bool? ThrowOnFail { get; set; }
    }
}