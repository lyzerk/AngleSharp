﻿namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents an HTML basefont element.
    /// Deprecated in HTML 4.01.
    /// </summary>
    [DomHistorical]
    sealed class HTMLBaseFontElement : HtmlElement
    {
        public HTMLBaseFontElement(Document owner)
            : base(owner, Tags.BaseFont, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
    }
}
