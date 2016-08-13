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
        /// Gets or sets the Gitter token used for authentication.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the Gitter Room Id.
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// Gets or sets the Gitter Incoming Web Hook Url
        /// </summary>
        /// <remarks>This can be used instead of token (https://developer.gitter.im/docs/rest-api)</remarks>
        public string IncomingWebHookUrl { get; set; }

        /// <summary>
        /// Gets or sets the Gitter Message Level
        /// </summary>
        /// <remarks>Default is Info</remarks>
        public GitterMessageLevel MessageLevel { get; set; }

        /// <summary>
        /// Gets or sets the Optional flag for if should throw exception on failure
        /// </summary>
        public bool? ThrowOnFail { get; set; }
    }
}