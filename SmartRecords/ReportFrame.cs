using System;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace SmartRecords
{
    public class ReportFrame : PdfPageEventHelper
    {
        readonly ReportFrameData config;

        public ReportFrame(ReportFrameData config)
        {
            this.config = config;
        }

        public override void OnOpenDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnOpenDocument(writer, document);
        }

        public override void OnChapter(PdfWriter writer, iTextSharp.text.Document document, float paragraphPosition, iTextSharp.text.Paragraph title)
        {
            base.OnChapter(writer, document, paragraphPosition, title);
        }

        public override void OnStartPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnStartPage(writer, document);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            if (writer == null) throw new ArgumentNullException("writer");
            if (document == null) throw new ArgumentNullException("document");

            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER, new Phrase(this.config.Header1, new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)), document.PageSize.Width / 2, document.PageSize.Height - 36, 0);
            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER, new Phrase(this.config.Header2, new Font(Font.FontFamily.HELVETICA, 14, Font.NORMAL, BaseColor.GRAY)), document.PageSize.Width / 2, document.PageSize.Height - 56, 0);

            writer.DirectContent.SetLineWidth(1);
            writer.DirectContent.SetColorFill(BaseColor.BLACK);
            writer.DirectContent.SetColorStroke(BaseColor.BLACK);
            writer.DirectContent.MoveTo(document.LeftMargin, document.PageSize.Height - document.TopMargin + 15);
            writer.DirectContent.LineTo(document.PageSize.Width - document.RightMargin, document.PageSize.Height - document.TopMargin + 15);
            writer.DirectContent.Stroke();

            writer.DirectContent.MoveTo(document.LeftMargin, document.BottomMargin - 15);
            writer.DirectContent.LineTo(document.PageSize.Width - document.RightMargin, document.BottomMargin - 15);
            writer.DirectContent.Stroke();

            // footer left 1
            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_LEFT, new Phrase(this.config.FooterLeft1, new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD)), document.LeftMargin, document.BottomMargin - 30, 0);

            // footer left 2
            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_LEFT, new Phrase(this.config.FooterLeft2, new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD)), document.LeftMargin, document.BottomMargin - 42, 0);

            // footer right 1
            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_RIGHT, new Phrase(this.config.FooterRight1, new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD)), document.PageSize.Width - document.RightMargin, document.BottomMargin - 30, 0);

            // footer right 2
            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_RIGHT, new Phrase(this.config.FooterRight2, new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD)), document.PageSize.Width - document.RightMargin, document.BottomMargin - 42, 0);

            base.OnEndPage(writer, document);
        }
    }
}