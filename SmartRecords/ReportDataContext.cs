using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using iTextSharp.text;

namespace SmartRecords
{
    public class ReportDataContext<TModel> : IDisposable, IReportDataContext
    {
        private readonly Document document;
        private readonly ReportSettings settings;
        private readonly TModel model;

        public ReportDataContext(Document document, ReportSettings settings, TModel model)
        {
            if (document == null) throw new ArgumentNullException("document");
            if (settings == null) throw new ArgumentNullException("settings");
            this.document = document;
            this.settings = settings;
            this.model = model;
            this.Components = new List<Tuple<LambdaExpression, ReportComponent>>();
        }

        /// <summary>
        /// Adds a new page breaker
        /// </summary>
        /// <returns></returns>
        public ReportNewPage AddNewPage()
        {
            var newPage = new ReportNewPage();
            this.Components.Add(new Tuple<LambdaExpression, ReportComponent>(null, newPage));
            return newPage;
        }

        /// <summary>
        /// Adds a new card to the report and returns it
        /// </summary>
        public ReportCard<TModel> AddCard()
        {
            var card = new ReportCard<TModel>();
            this.Components.Add(new Tuple<LambdaExpression, ReportComponent>(null, card));
            return card;
        }

        /// <summary>
        /// Adds a new card to the report and returns it
        /// </summary>
        public ReportCard<TPanelModel> AddCard<TPanelModel>(Expression<Func<TModel, TPanelModel>> expressionModel)
        {
            if (expressionModel == null) throw new ArgumentNullException("expressionModel");
            var card = new ReportCard<TPanelModel>();
            this.Components.Add(new Tuple<LambdaExpression, ReportComponent>(expressionModel, card));
            return card;
        }

        /// <summary>
        /// Adds a new card to the report and returns it
        /// </summary>
        public ReportGrid<TGridModel> AddGrid<TGridModel>(Expression<Func<TModel, IEnumerable<TGridModel>>> expressionModel)
        {
            if (expressionModel == null) throw new ArgumentNullException("expressionModel");
            var grid = new ReportGrid<TGridModel>();
            this.Components.Add(new Tuple<LambdaExpression, ReportComponent>(expressionModel, grid));
            return grid;
        }

        /// <summary>
        /// Adds a new title to the report and returns it
        /// </summary>
        public ReportTitle<TModel> AddTitle(ReportTitleSize size, string leftAlignedText, string rightAlignedText = null)
        {
            if (leftAlignedText == null) throw new ArgumentNullException("leftAlignedText");
            var title = new ReportTitle<TModel>(size, leftAlignedText, rightAlignedText);
            this.Components.Add(new Tuple<LambdaExpression, ReportComponent>(null, title));
            return title;
        }

        /// <summary>
        /// Adds a new title to the report and returns it
        /// </summary>
        public ReportTitle<TModel> AddTitle(ReportTitleSize size, Func<TModel, string> leftAlignedTextGetter, Func<TModel, string> rightAlignedTextGetter = null)
        {
            if (leftAlignedTextGetter == null) throw new ArgumentNullException("leftAlignedTextGetter");
            var title = new ReportTitle<TModel>(size, leftAlignedTextGetter, rightAlignedTextGetter);
            this.Components.Add(new Tuple<LambdaExpression, ReportComponent>(null, title));
            return title;
        }

        protected List<Tuple<LambdaExpression, ReportComponent>> Components { get; set; }

        /// <summary>
        /// Will render all the report components
        /// </summary>
        public void Render()
        {
            foreach (var panelTuple in this.Components)
            {
                var expression = panelTuple.Item1;
                var component = panelTuple.Item2;

                object panelExpressionValue = null;
                if (expression != null)
                {
                    var panelExpression = (Func<TModel, object>)expression.Compile();
                    panelExpressionValue = panelExpression(model);
                }
                else
                    panelExpressionValue = model;

                component.Render(panelExpressionValue, this.document, settings);
            }
        }

        public void Dispose()
        {
            this.Render();
        }
    }
}
