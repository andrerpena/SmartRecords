using System.Diagnostics;

namespace SmartRecords.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var frame = new ReportFrame(
                new ReportFrameData()
                {
                    Header1 = "Contacts list sample",
                    Header2 = "Created by André Pena's SmartRecords",
                    FooterLeft1 = "Created by SmartRecords",
                    FooterLeft2 = "http://www.smartrecords.net",
                    FooterRight1 = "",
                    FooterRight2 = ""
                });

            // creates a new report
            var report = new Report(frame);
            foreach (var patient in ContactsDataSource.GetContacts())
            {
                // creates a data-context for each contact inside the report
                // inside the data-context, it's possible to add titles, cards and grids
                using (var patientContext = report.AddDataContext(patient))
                {
                    patientContext.AddTitle(ReportTitleSize.H1, m => "Contact: " + m.FirstName + " " + m.LastName);
                    var card = patientContext.AddCard();
                    card.AddField(m => m.FirstName);
                    card.AddField(m => m.LastName);
                    card.AddField(m => m.DateOfBirth);
                    card.AddField(m => m.Email, true);
                    card.AddField(m => m.Notes);

                    patientContext.AddTitle(ReportTitleSize.H2, m => "Address");
                    var addressCard = patientContext.AddCard(m => m.Address);
                    addressCard.AddField(m => m.AddressLine1, true);
                    addressCard.AddField(m => m.AddressLine2, true);
                    addressCard.AddField(m => m.City);
                    addressCard.AddField(m => m.State);
                    addressCard.AddField(m => m.Country);
                    addressCard.AddField(m => m.Zip);

                    patientContext.AddTitle(ReportTitleSize.H2, m => "Appointments");
                    var appointmentCard = patientContext.AddGrid(m => m.Appointments);
                    appointmentCard.AddColumn(m => m.With);
                    appointmentCard.AddColumn(m => m.Date);
                }
            }
            report.SaveToFile("document.pdf");
            Process.Start("document.pdf");
        }
    }
}
