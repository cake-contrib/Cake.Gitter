using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Gitter
{
    /// <summary>
    /// <para>Contains aliases related to <see href="https://gitter.im">gitter.im</see>.</para>
    /// <para>
    /// In order to use the commands for this addin, you will need to include the following in your build.cake file to download and
    /// reference from NuGet.org:
    /// <code>
    /// #addin Cake.Gitter
    /// </code>
    /// </para>
    /// </summary>
    [CakeAliasCategory("Gitter")]
    public static class GitterAliases
    {
        /// <summary>
        /// Gets a <see cref="GitterProvider"/> instance that can be used for communicating with GitterProvider API.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="GitterProvider"/> instance.</returns>
        [CakePropertyAlias(Cache = true)]
        [CakeNamespaceImport("Cake.Gitter.Chat")]
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