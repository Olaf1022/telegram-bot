﻿using SKitLs.Bots.Telegram.Core.Model.UpdatesCasting.Signed;

namespace SKitLs.Bots.Telegram.Core.Model.Interactions.Defaults
{
    // XML-Doc Update
    /// <summary>
    /// Default realization of <see cref="IBotAction"/>&lt;<see cref="SignedCallbackUpdate"/>&gt;
    /// used for handling callbacks and executing them.
    /// </summary>
    public class DefaultCallback : DefaultBotAction<SignedCallbackUpdate>
    {
        /// <summary>
        /// Display label for inline keyboards markup.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Creates a new instance of a <see cref="DefaultCallback"/> with specific data of <see cref="LabeledData"/>.
        /// </summary>
        /// <param name="data">Specific labeled data pair.</param>
        /// <param name="action">An action to be executed.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DefaultCallback(LabeledData data, BotInteraction<SignedCallbackUpdate> action)
            : this(data.Data, data.Label, action) { }
        /// <summary>
        /// Creates a new instance of a <see cref="DefaultCallback"/> with specific data.
        /// </summary>
        /// <param name="base">Action name base.</param>
        /// <param name="label">A label to be displayed.</param>
        /// <param name="action">An action to be executed.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DefaultCallback(string @base, string label, BotInteraction<SignedCallbackUpdate> action)
            : base(@base, action) => Label = label;

        /// <summary>
        /// UNSAFE. Creates a new instance of a <see cref="DefaultCallback"/>
        /// with specific data. Use to avoid compiler errors when passing non-static methods
        /// to base() constructor for an action.
        /// <para>Do not forget to override <see cref="DefaultBotAction{TUpdate}.Action"/> property.</para>
        /// </summary>
        /// <param name="base">Action name base.</param>
        /// <param name="label">A label to be displayed.</param>
        /// <exception cref="ArgumentNullException"></exception>
        [Obsolete("Do not forget to override Action property")]
        protected DefaultCallback(string @base, string label) : base(@base) => Label = label;

        /// <summary>
        /// Checks either this action should be executed on a certain incoming update.
        /// </summary>
        /// <param name="update">An incoming update.</param>
        /// <returns><see langword="true"/> if this action should be executed; otherwise, <see langword="false"/>.</returns>
        public override bool ShouldBeExecutedOn(SignedCallbackUpdate update) => ActionNameBase == update.Data;

        /// <summary>
        /// Returns a string that represents current object.
        /// </summary>
        /// <returns>A string that represents current object.</returns>
        public override string ToString() => $"[{GetType().Name}] \"{Label}\" - {ActionNameBase}";
    }
}