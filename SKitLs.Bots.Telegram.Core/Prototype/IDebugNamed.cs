﻿namespace SKitLs.Bots.Telegram.Core.Prototype
{
    // XML-Doc Update
    /// <summary>
    /// An interface that provides methods to simplify debugging process.
    /// </summary>
    public interface IDebugNamed
    {
        /// <summary>
        /// Name, used for simplifying debugging process.
        /// </summary>
        public string? DebugName { get; }
    }
}