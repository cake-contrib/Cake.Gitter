using Cake.Core;

namespace Cake.Gitter
{
    /// <summary>
    /// Contains functionality related to Gitter API
    /// </summary>
    public sealed class GitterProvider
    {
        private readonly object _chat;

        /// <summary>
        /// The Slack Chat functionality.
        /// </summary>
        public object Chat { get { return _chat; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="GitterProvider"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GitterProvider(ICakeContext context)
        {
            _chat = new object();
        }
    }
}