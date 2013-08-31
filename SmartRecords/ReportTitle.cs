using System;
using iTextSharp.text;
using iTextSharp.text.pdf.draw;

namespace SmartRecords
{
    public class ReportTitle<TModel> : ReportComponent
    {
        /// <summary>
        /// Constructs a new title with static texts
        /// </summary>
        public ReportTitle(ReportTitleSize size, string leftAlignedText, string rightAlignedText = null)
        {
            if (leftAlignedText == null) throw new ArgumentNullException("leftAlignedText");
            this.Size = size;
            this.LeftAlignedText = leftAlignedText;
            this.RightAlignedText = rightAlignedText;
        }

        /// <summary>
        /// Constructs a new title with dynamic texts
        /// </summary>
        public ReportTitle(ReportTitleSize size, Func<TModel, string> leftAlignedTextGetter, Func<TModel, string> rightAlignedTextGetter = null)
        {
            if (leftAlignedTextGetter == null) throw new ArgumentNullException("leftAlignedTextGetter");
            this.Size = size;
            this.LeftAlignedTextGetter = leftAlignedTextGetter;
            this.RightAlignedTextGetter = rightAlignedTextGetter;
        }

        public ReportTitleSize Size { get; set; }
        public Func<TModel, string> LeftAlignedTextGetter { get; set; }
        public Func<TModel, string> RightAlignedTextGetter { get; set; }
        public string LeftAlignedText { get; set; }
        public string RightAlignedText { get; set; }

        public override void Render(object model, Document document, ReportSettings settings)
        {
            var leftAlignedText = this.LeftAlignedText;
            if (string.IsNullOrEmpty(leftAlignedText))
                leftAlignedText = this.LeftAlignedTextGetter((TModel)model);
            var rightAlignedText = this.RightAlignedText;
            if (string.IsNullOrEmpty(rightAlignedText) && this.RightAlignedTextGetter != null)
                rightAlignedText = this.RightAlignedTextGetter((TModel) model);

            var titleParagraph = new Paragraph
                {
                    new Phrase(leftAlignedText, this.GetFont(this.Size, true, settings)),
                    new Chunk(new LineSeparator(0, 100, BaseColor.WHITE, Element.ALIGN_CENTER, 0)),
                    new Phrase(rightAlignedText, this.GetFont(this.Size, false, settings)),
                };

            if (this.GetBorderWidth(this.Size, settings) > 0)
            {
                titleParagraph.Add(new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_CENTER, -3));
                titleParagraph.SpacingAfter = 8;
            }

            document.Add(titleParagraph);
        }

        /// <summary>
        /// Returns the font based on settings
        /// </summary>
        private Font GetFont(ReportTitleSize size, bool leftText, ReportSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            switch (size)
            {
                case ReportTitleSize.H1:
                    return leftText ? settings.TitleH1LeftAlignedTextFont : settings.TitleH1RightAlignedTextFont;
                case ReportTitleSize.H2:
                    return leftText ? settings.TitleH2LeftAlignedTextFont : settings.TitleH2RightAlignedTextFont;
                case ReportTitleSize.H3:
                    return leftText ? settings.TitleH3LeftAlignedTextFont : settings.TitleH3RightAlignedTextFont;
            }
            throw new Exception("Unsuported size");
        }

        /// <summary>
        /// Returns the border width based on settings
        /// </summary>
        /// <param name="size"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private float GetBorderWidth(ReportTitleSize size, ReportSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            switch (size)
            {
                case ReportTitleSize.H1:
                    return settings.TitleH1BorderWidth;
                case ReportTitleSize.H2:
                    return settings.TitleH2BorderWidth;
                case ReportTitleSize.H3:
                    return settings.TitleH3BorderWidth;
            }
            throw new Exception("Unsuported size");
        }
    }

    public enum ReportTitleSize
    {
        H1,
        H2,
        H3
    }
}