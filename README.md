SmartRecords
============

SmartRecords is a ridiculously simple library for generating PDF reports out of .NET object models. It's built on top of iTextSharp, which is a problem because it's not free for commercial use (your project must be AGPL compatible). Depending on the SmartRecords acceptance, I may try to implement a renderer using some commercial-use compatible PDF library. Let's see what happens.

SmartRecords is ideal for exporting data for storage or printing. It's good because it's simple, strongly-typed, designer-free and very fast.

The package you download comes with a sample application that explores all the currently available functionalities (not many). All you have to do is to download it and run the `SmartRecords.Sample`. The generated PDF will pop up. All dependencies are included and no setup is required.

Usage
-----

First thing to do is to define a frame for your report, that is, determine what will be displayed in the header and the footer of your report pages. SmartRecords comes with a simple implementation called `ReportFrame`, but you can, of course, roll your own.

Example of a `ReportFrame` definition:
```csharp
var frame = new ReportFrame(
	new ReportFrameData()
	{
		  Header1 = "Contacts list sample",
		  Header2 = "Created by André Pena's SmartRecords",
		  FooterLeft1 = "Created by SmartRecords",
		  FooterLeft2 = "http://www.smartrecords.net",
		  FooterRight1 = "",
		  FooterRight2 = ""
	}
);
```

To create a new report, just do this:

```csharp
var report = new Report(frame);
foreach (var contact in ContactsDataSource.GetContacts())
{
	using (var contactContext = report.AddDataContext(contact))
	{
		// the contactContext has been created
		// you can now add COMPONENTS to it
	}
}
```

