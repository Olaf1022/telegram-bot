﻿namespace SKitLs.Bots.Telegram.BotProcesses.Prototype.Processes
{
    /// <summary>
    /// Represents an interface for a sub-process definition within a parent bot running process.
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner (parent) bot running process, implementing <see cref="IBotRunningProcess"/>.</typeparam>
    public interface ISubProcess<TOwner> where TOwner : IBotRunningProcess
    {
        /// <summary>
        /// Represents the order of the sub-process within the parent bot running process.
        /// </summary>
        public int SubOrder { get; }
        /// <summary>
        /// Determines whether this sub-process is terminational.
        /// </summary>
        public bool IsTerminational { get; }

        /// <summary>
        /// Creates new running bot process instance based on the specified user and arguments.
        /// </summary>
        /// <param name="owner">Parent bot running process, that has raised running.</param>
        /// <returns>The running bot process instance representing the ongoing execution of the process.</returns>
        public ISubRunning<TOwner> GetRunning(TOwner owner);
    }
}