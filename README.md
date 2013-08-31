SmartRecords
============

SmartRecords is a ridiculously simple library for generating PDF reports out of .NET object models. It's built on top of iTextSharp, which is a problem because it's not free for commercial use (your project must be AGPL compatible). Depending on the SmartRecords acceptance, I may try to implement a renderer using some commercial-use compatible PDF library. Let's see what happens.

SmartRecords is ideal for exporting data for storage or printing. It's good because it's simple, strongly-typed, designer-free and very fast.

The package you download comes with a sample application that explores all the currently available functionalities (not many). All you have to do is to download it and run the `SmartRecords.Sample`. The generated PDF will pop up. All dependencies are included and no setup is required.

Usage
-----

First thing to do is to define a frame for your report, that is, determine what will be displayed in the header and the footer of your report pages. SmartRecords comes with a simple implementation called `ReportFrame`, but you can, of course, roll your own.

You create a report by instantiating the report `Report` class. Inside the report, you create data contexts to which components can be added. There are 3 components: `Card`, `Grid` and `Title` which are self explanatory.

After the report is finished, just call `SaveToFile` to export a PDF file.

```csharp
static void Main(string[] args)
{
	// the frame determines what is displayed in the header and in the
	// footer of your report pages
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
	foreach (var contact in ContactsDataSource.GetContacts())
	{
		// creates a data-context for each contact inside the report.
		// a data context is a section of your report that is bound to
		// a particular data object.
		using (var contactContext = report.AddDataContext(contact))
		{
			// creates a title for the contact
			contactContext.AddTitle(ReportTitleSize.H1, m => "Contact: " + m.FirstName + " " + m.LastName);

			// creates a Card to display the contacts details
			var card = contactContext.AddCard();
			card.AddField(m => m.FirstName);
			card.AddField(m => m.LastName);
			card.AddField(m => m.DateOfBirth);
			card.AddField(m => m.Email, true);
			card.AddField(m => m.Notes);

			// creates another Card for displaying the contact's address details
			contactContext.AddTitle(ReportTitleSize.H2, m => "Address");
			var addressCard = contactContext.AddCard(m => m.Address);
			addressCard.AddField(m => m.AddressLine1, true);
			addressCard.AddField(m => m.AddressLine2, true);
			addressCard.AddField(m => m.City);
			addressCard.AddField(m => m.State);
			addressCard.AddField(m => m.Country);
			addressCard.AddField(m => m.Zip);

			// creates a Grid for displaying the contact's appointments
			contactContext.AddTitle(ReportTitleSize.H2, m => "Appointments");
			var appointmentCard = contactContext.AddGrid(m => m.Appointments);
			appointmentCard.AddColumn(m => m.With);
			appointmentCard.AddColumn(m => m.Date);
		}
	}

	report.SaveToFile("document.pdf");
	Process.Start("document.pdf");
}
```
