using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Slack
{
    /// <summary>
    /// Contains aliases related to GitterProvider API
    /// </summary>
    [CakeAliasCategoryAttribute("Slack")]
    public static class GitterAliases
    {
        /// <summary>
        /// Gets a <see cref="GitterProvider"/> instance that can be used for communicating with GitterProvider API.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="GitterProvider"/> instance.</returns>
        [CakePropertyAlias(Cache = true)]
        [CakeNamespaceImportAttribute("Cake.Gitter.Chat")]
        public static GitterProvider Gitter(this ICakeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return new GitterProvider(context);
        }
    }
}