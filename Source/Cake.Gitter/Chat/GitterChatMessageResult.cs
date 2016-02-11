﻿using System.Text;
using Cake.Core.Annotations;

namespace Cake.Gitter.Chat
{
    /// <summary>
    /// The result of GitterProvider Chat API post
    /// </summary>
    [CakeAliasCategory("Gitter")]
    public sealed class GitterChatMessageResult
    {
        private readonly bool _ok;
        private readonly string _timeStamp;
        private readonly string _error;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitterChatMessageResult"/> class.
        /// </summary>
        /// <param name="ok">Indicating success or failure</param>
        /// <param name="timeStamp">Timestamp of the message</param>
        /// <param name="error">Error message on failure</param>
        public GitterChatMessageResult(bool ok, string timeStamp, string error)
        {
            _ok = ok;
            _timeStamp = timeStamp;
            _error = error;
        }

        /// <summary>
        /// Gets a value indicating whether success or failure, <see cref="Error"/> for info on failure
        /// </summary>
        public bool Ok
        {
            get
            {
                return _ok;
            }
        }

        /// <summary>
        /// Gets the Timestamp of the message
        /// </summary>
        public string TimeStamp
        {
            get
            {
                return _timeStamp;
            }
        }

        /// <summary>
        /// Gets the Error message on failure
        /// </summary>
        public string Error
        {
            get
            {
                return _error;
            }
        }

        /// <summary>
        /// Convert this instance of value to a string representation
        /// </summary>
        /// <returns>The complete string representation of the GitterChatMessageResult</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("{ Ok = ");
            builder.Append(Ok);
            builder.Append(", TimeStamp = ");
            builder.Append(TimeStamp);
            builder.Append(", Error = ");
            builder.Append(Error);
            builder.Append(" }");
            return builder.ToString();
        }
    }
}