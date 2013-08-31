using System;
using System.Linq.Expressions;

namespace SmartRecords
{
    public class ReportGridColumn<TModel>
    {
        public Expression<Func<TModel, object>> Expression { get; set; }
    }
}
