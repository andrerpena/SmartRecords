using System.Collections.Generic;
using System.IO;
using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SmartRecords
{
    public class Report
    {
        private readonly Document document;
        private readonly MemoryStream documentStream;

        public ReportFrame Frame { get; set; }

        List<IReportDataContext> DataContexts { get; set; }

        public Report(ReportFrame frame = null, ReportSettings settings = null)
        {
            this.Frame = frame;
            this.Settings = settings ?? new ReportSettings();
            this.DataContexts = new List<IReportDataContext>();
            this.document = new Document(PageSize.A4, 36, 36, 80, 80);
            var art = new Rectangle(50, 50, 545, 792);
            this.documentStream = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, documentStream);

            if (frame != null)
            {
                writer.SetBoxSize("art", art);
                writer.PageEvent = frame;
            }

            writer.CloseStream = false;
            document.Open();
        }

        protected ReportSettings Settings { get; set; }

        public ReportDataContext<TModel> AddDataContext<TModel>(TModel model)
        {
            var dataContext = new ReportDataContext<TModel>(this.document, this.Settings, model);
            this.DataContexts.Add(dataContext);
            return dataContext;
        }

        public MemoryStream Stream
        {
            get
            {
                return this.documentStream;
            }
        }

        /// <summary>
        /// Saves report to a file
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveToFile(string filePath)
        {
            this.document.Close();
            this.documentStream.Seek(0, SeekOrigin.Begin);

            using (Stream file = File.Open(filePath, FileMode.Create))
            {
                var buffer = new byte[8 * 1024];
                int len;
                while ((len = this.documentStream.Read(buffer, 0, buffer.Length)) > 0)
                    file.Write(buffer, 0, len);
            }
        }
    }
}