using ReportGenerator;
using iTextSharp.text;

namespace SmartRecords
{
    public abstract class ReportComponent
    {
        public abstract void Render(object model, Document document, ReportSettings settings);
    }
}