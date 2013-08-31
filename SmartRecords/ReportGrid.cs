using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SmartRecords
{
    public class ReportGrid<TModel> : ReportComponent
    {
        protected List<ReportGridColumn<TModel>> Columns { get; set; }

        public ReportGrid()
        {
            this.Columns = new List<ReportGridColumn<TModel>>();
        }

        public void AddColumn(Expression<Func<TModel, object>> columnExpression)
        {
            if (columnExpression == null) throw new ArgumentNullException("columnExpression");
            this.Columns.Add(new ReportGridColumn<TModel> { Expression = columnExpression });
        }

        public override void Render(object model, Document document, ReportSettings settings)
        {
            if (model == null) throw new ArgumentNullException("model");
            if (document == null) throw new ArgumentNullException("document");

            var modelAsEnumerable = (IEnumerable<TModel>)model;

            var table = new PdfPTable(this.Columns.Count)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 8,
                    SpacingAfter = 8,
                };

            // add headers
            foreach (var labelText in this.Columns.Select(field => ExpressionHelper.GetDisplayName(field.Expression)))
                table.AddCell(new PdfPCell(new Phrase() { new Chunk(labelText, settings.GridHeaderFont) })
                    {
                        Padding = settings.GridHeaderPadding,
                        BackgroundColor = new BaseColor(settings.GridHeaderBackgroundColor),
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        BorderColor = new BaseColor(settings.GridHeaderBorderColor)
                    });

            // add data
            foreach (var datum in modelAsEnumerable)
            {
                foreach (var column in this.Columns)
                {
                    var value = column.Expression.Compile()(datum);
                    var valueText = value != null ? value.ToString() : null;

                    table.AddCell(new PdfPCell(new Phrase() { new Chunk(valueText, settings.GridBodyFont) })
                    {
                        Padding = settings.GridBodyPadding,
                        BackgroundColor = new BaseColor(settings.GridBodyBackgroundColor),
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        BorderColor = new BaseColor(settings.GridBodyBorderColor)
                    });
                }
            }

            document.Add(table);
        }
    }
}