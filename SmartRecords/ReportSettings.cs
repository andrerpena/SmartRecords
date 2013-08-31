using System.Drawing;
using Font = iTextSharp.text.Font;

namespace SmartRecords
{
    public class ReportSettings
    {
        public ReportSettings()
        {
            // Card properties
            this.CardValueFont = new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL);
            this.CardLabelFont = new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD);
            this.CardBorderColor = Color.Gray;
            this.CardCellPadding = 3;
            this.CardBorderWidth = 0.5f;

            // Title properties
            this.TitleH1LeftAlignedTextFont = new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD);
            this.TitleH1RightAlignedTextFont = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD);
            this.TitleH1BorderWidth = 1f;


            this.TitleH2LeftAlignedTextFont = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD);
            this.TitleH2RightAlignedTextFont = new Font(Font.FontFamily.HELVETICA, 19f, Font.BOLD);
            this.TitleH2BorderWidth = 0f;

            this.TitleH3LeftAlignedTextFont = new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLDITALIC);
            this.TitleH3RightAlignedTextFont = new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLDITALIC);
            this.TitleH3BorderWidth = 0f;

            // Grid properties
            this.GridHeaderBorderColor = Color.Gray;
            this.GridHeaderFont = new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD);
            this.GridHeaderPadding = 3;
            this.GridHeaderBackgroundColor = System.Drawing.ColorTranslator.FromHtml("#cccccc");
            this.GridBodyBorderColor = Color.Gray;
            this.GridBodyFont = new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL);
            this.GridBodyPadding = 3;
            this.GridBodyBackgroundColor = Color.White;
        }

        // Card properties
        public float CardBorderWidth { get; set; }
        public Color CardBorderColor { get; set; }
        public Font CardValueFont { get; set; }
        public Font CardLabelFont { get; set; }
        public float CardCellPadding { get; set; }

        // Title properties
        public Font TitleH1LeftAlignedTextFont { get; set; }
        public Font TitleH1RightAlignedTextFont { get; set; }
        public float TitleH1BorderWidth { get; set; }

        public Font TitleH2LeftAlignedTextFont { get; set; }
        public Font TitleH2RightAlignedTextFont { get; set; }
        public float TitleH2BorderWidth { get; set; }

        public Font TitleH3LeftAlignedTextFont { get; set; }
        public Font TitleH3RightAlignedTextFont { get; set; }
        public float TitleH3BorderWidth { get; set; }

        // Grid properties
        public Color GridHeaderBorderColor { get; set; }
        public Font GridHeaderFont { get; set; }
        public float GridHeaderPadding { get; set; }
        public Color GridHeaderBackgroundColor { get; set; }
        public Color GridBodyBorderColor { get; set; }
        public Font GridBodyFont { get; set; }
        public float GridBodyPadding { get; set; }
        public Color GridBodyBackgroundColor { get; set; }
    }
}
