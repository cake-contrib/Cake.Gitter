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
        /// Gitter token used for authentication.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// This can be used instead of token (https://developer.gitter.im/docs/rest-api)
        /// </summary>
        public string IncomingWebHookUrl { get; set; }

        /// <summary>
        /// Name of bot.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Optional flag for if should throw exception on failure
        /// </summary>
        public bool? ThrowOnFail { get; set; }
    }
}