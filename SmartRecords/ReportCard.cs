using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ReportGenerator;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SmartRecords
{
    public class ReportCard<TModel> : ReportComponent
    {
        protected List<ReportCardField<TModel>> Fields { get; set; }

        protected int FieldsPerRow { get; set; }

        public ReportCard(int fieldsPerRow = 2)
        {
            this.Fields = new List<ReportCardField<TModel>>();
            this.FieldsPerRow = fieldsPerRow;
        }

        public void AddField(Expression<Func<TModel, object>> fieldExpression, bool wholeRow = false)
        {
            if (fieldExpression == null) throw new ArgumentNullException("fieldExpression");
            this.Fields.Add(new ReportCardField<TModel>
            {
                Expression = fieldExpression,
                WholeRow = wholeRow
            });
        }

        public override void Render(object model, Document document, ReportSettings settings)
        {
            if (model == null) throw new ArgumentNullException("model");
            if (document == null) throw new ArgumentNullException("document");

            var table = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 8,
                    SpacingAfter = 8
                };

            var cellIndex = 0;
            var rowIndex = 0;

            for (var fieldIndex = 0; fieldIndex < this.Fields.Count; fieldIndex++)
            {
                var field = this.Fields[fieldIndex];

                var labelText = ExpressionHelper.GetDisplayName(field.Expression);
                var value = field.Expression.Compile()((TModel)model);
                var valueText = value != null ? value.ToString() : null;

                // adds the label cell
                table.AddCell(new PdfPCell(new Phrase() { new Chunk(labelText + ":", settings.CardLabelFont) })
                    {
                        Padding = settings.CardCellPadding,
                        BorderColor = new BaseColor(settings.CardBorderColor),
                        BorderWidthTop = rowIndex == 0 ? settings.CardBorderWidth : 0,
                        BorderWidthRight = 0,
                        BorderWidthBottom = settings.CardBorderWidth,
                        BorderWidthLeft = cellIndex == 0 ? settings.CardBorderWidth : 0,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    });

                // adds the value cell
                table.AddCell(new PdfPCell(new Phrase() { new Chunk(valueText, settings.CardValueFont) })
                    {
                        Padding = settings.CardCellPadding,
                        BorderColor = new BaseColor(settings.CardBorderColor),
                        BorderWidthTop = rowIndex == 0 ? settings.CardBorderWidth : 0,
                        BorderWidthRight = settings.CardBorderWidth,
                        BorderWidthBottom = settings.CardBorderWidth,
                        BorderWidthLeft = 0,
                        Colspan = this.GetColspan(field, fieldIndex, cellIndex)
                    });

                // computes cellIndex for the next field
                if (field.WholeRow)
                {
                    cellIndex = 0;
                    rowIndex++;
                }
                else if (fieldIndex < (this.Fields.Count - 1) && this.Fields[fieldIndex + 1].WholeRow)
                {
                    cellIndex = 0;
                    rowIndex++;
                }
                else if (cellIndex == this.FieldsPerRow - 1)
                {
                    cellIndex = 0;
                    rowIndex++;
                }
                else
                    cellIndex++;
            }

            document.Add(table);
        }

        /// <summary>
        /// Returns the colspan for a value cell in the card table
        /// </summary>
        private int GetColspan(ReportCardField<TModel> field, int fieldIndex, int cellIndex)
        {
            var completeRowColSpan = (this.FieldsPerRow - cellIndex) * 2 - 1;
            // if current field itself is whole row
            if (field.WholeRow)
                return completeRowColSpan;
            // if next field is wholerow
            if (fieldIndex < (this.Fields.Count - 1) && this.Fields[fieldIndex + 1].WholeRow)
                return completeRowColSpan;
                // if the current field is in the last row and needs to fullfill it.
            if (fieldIndex == this.Fields.Count - 1)
                return completeRowColSpan;

            return 1;
        }
    }
}