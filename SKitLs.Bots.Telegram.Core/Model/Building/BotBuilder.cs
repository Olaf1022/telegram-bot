﻿using SKitLs.Bots.Telegram.Core.Model.DelieverySystem;
using SKitLs.Bots.Telegram.Core.resources.Settings;
using Telegram.Bot.Types.Enums;

namespace SKitLs.Bots.Telegram.Core.Model.Building
{
    /// <summary>
    /// Bot creating proccess enter point. <see cref="BotManager"/> class wizard constructor.
    /// </summary>
    public class BotBuilder
    {
        /// <summary>
        /// Bot's debug settings.
        /// </summary>
        public static DebugAssets DebugAssets { get; private set; }

        static BotBuilder() => DebugAssets = new();

        /// <summary>
        /// Constructing instance.
        /// </summary>
        private readonly BotManager _botManager;
        /// <summary>
        /// Creates a new instance of the wizard constructor.
        /// </summary>
        /// <param name="token">Telegram bot's token</param>
        private BotBuilder(string token, DebugAssets? assests = null)
        {
            if (assests is not null)
                DebugAssets = assests;
            _botManager = new(token);
        }
        /// <summary>
        /// Creates a new instance of the wizard constructor.
        /// </summary>
        /// <param name="token">Telegram bot's token</param>
        public static BotBuilder NewBuilder(string token) => new(token);

        /// <summary>
        /// Enables private chats' handling. Uses vanilla <see cref="ChatScanner"/> by default.
        /// </summary>
        /// <param name="builder">Customized <see cref="ChatScanner"/> desiner. Can be null to use default.</param>
        public BotBuilder EnablePrivates(ChatDesigner? builder = null)
        {
            _botManager.PrivateChatUpdateHandler = builder?.Build() ?? new();
            _botManager.PrivateChatUpdateHandler.ChatType = ChatType.Private;
            _botManager.PrivateChatUpdateHandler.DebugName = nameof(_botManager.PrivateChatUpdateHandler);
            return this;
        }
        /// <summary>
        /// Enables groups' handling. Uses vanilla <see cref="ChatScanner"/> by default.
        /// </summary>
        /// <param name="builder">Customized <see cref="ChatScanner"/> desiner. Can be null to use default.</param>
        public BotBuilder EnableGroupWith(ChatDesigner? builder = null)
        {
            _botManager.GroupChatUpdateHandler = builder?.Build() ?? new();
            _botManager.GroupChatUpdateHandler.ChatType = ChatType.Private;
            _botManager.GroupChatUpdateHandler.DebugName = nameof(_botManager.GroupChatUpdateHandler);
            return this;
        }
        /// <summary>
        /// Enables supergroups' handling. Uses vanilla <see cref="ChatScanner"/> by default.
        /// </summary>
        /// <param name="builder">Customized <see cref="ChatScanner"/> desiner. Can be null to use default.</param>
        public BotBuilder EnableSupergroupsWith(ChatDesigner? builder = null)
        {
            _botManager.SupergroupChatUpdateHandler = builder?.Build() ?? new();
            _botManager.SupergroupChatUpdateHandler.ChatType = ChatType.Private;
            _botManager.SupergroupChatUpdateHandler.DebugName = nameof(_botManager.SupergroupChatUpdateHandler);
            return this;
        }
        /// <summary>
        /// Enables channels' handling. Uses vanilla <see cref="ChatScanner"/> by default.
        /// </summary>
        /// <param name="builder">Customized <see cref="ChatScanner"/> desiner. Can be null to use default.</param>
        public BotBuilder EnableChannelsWith(ChatDesigner? builder = null)
        {
            _botManager.ChannelChatUpdateHandler = builder?.Build() ?? new();
            _botManager.ChannelChatUpdateHandler.ChatType = ChatType.Private;
            _botManager.ChannelChatUpdateHandler.DebugName = nameof(_botManager.ChannelChatUpdateHandler);
            return this;
        }

        /// <summary>
        /// Sets custom <see cref="IDelieveryService"/> for messages sending.
        /// </summary>
        /// <param name="delievery">Custom service to be implemented</param>
        public BotBuilder CustomDelievery(IDelieveryService delievery)
        {
            _botManager.DelieveryService = delievery;
            return this;
        }

        /// <summary>
        /// Adds a new service of a type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Interface type of a service</typeparam>
        /// <param name="service">Service to be stored</param>
        public BotBuilder AddService<T>(T service) where T : notnull
        {
            _botManager.AddService(service);
            return this;
        }

        /// <summary>
        /// Compiles created instance and returns the built one.
        /// </summary>
        /// <param name="debugName">Custom debug name (<see cref="BotManager.DebugName"/>)</param>
        public BotManager Build(string? debugName = null)
        {
            _botManager.DebugName = debugName;
            _botManager.ReflectiveCompile();
            _botManager.CollectActionsBasket();
            _botManager.AddService(DebugAssets.Localizator);
            _botManager.AddService(DebugAssets.LocalLogger);
            return _botManager;
        }
    }
}