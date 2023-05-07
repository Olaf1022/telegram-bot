﻿using SKitLs.Bots.Telegram.Core.Prototypes;

namespace SKitLs.Bots.Telegram.PageNavs.Prototype
{
    public interface IPageWrap
    {
        public string PageID { get; }
        public IOutputMessage Source { get; }

        public IOutputMessage BuildMessage(IPageWrap previous);
        public string GetLabel();
    }
}