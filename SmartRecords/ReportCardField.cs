using System;
using System.Linq.Expressions;

namespace SmartRecords
{
    public class ReportCardField<TModel>
    {
        public Expression<Func<TModel, object>> Expression { get; set; }
        public bool WholeRow { get; set; }
    }
}
