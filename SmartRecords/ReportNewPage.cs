using iTextSharp.text;

namespace SmartRecords
{
    /// <summary>
    /// Creates a new page in the report
    /// </summary>
    public class ReportNewPage : ReportComponent
    {
        public override void Render(object model, Document document, ReportSettings settings)
        {
            document.NewPage();
        }
    }
}
