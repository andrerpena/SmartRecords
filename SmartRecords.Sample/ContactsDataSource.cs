using System;
using System.Collections.Generic;
using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using SmartRecords.Sample.Model;

namespace SmartRecords.Sample
{
    /// <summary>
    /// Data-source for contacts
    /// </summary>
    public class ContactsDataSource
    {
        public static IList<Contact> GetContacts()
        {
            // Perform factory set up (once for entire test run)
            var factory = AutoPocoContainer.Configure(x =>
                {
                    x.Conventions(c => c.UseDefaultConventions());
                    x.AddFromAssemblyContainingType<Contact>();
                    x.Include<Contact>()
                     .Setup(c => c.FirstName).Use<FirstNameSource>()
                     .Setup(c => c.LastName).Use<LastNameSource>()
                     .Setup(c => c.DateOfBirth).Use<DateOfBirthSource>()
                     .Setup(c => c.Email).Use<EmailAddressSource>()
                     .Setup(c => c.Notes).Use<LoremIpsumSource>()
                     .Setup(c => c.Appointments).Use<AppointmentDataSource>();
                    x.Include<Address>()
                     .Setup(m => m.AddressLine1).Use<AddressLine1Source>()
                     .Setup(m => m.Country).Use<ValueSource<string>>("United States")
                     .Setup(m => m.State).Use<UsStatesSource>()
                     .Setup(m => m.Zip).Use<RandomStringSource>(10, 10)
                     .Setup(m => m.Country);
                    x.Include<Appointment>()
                     .Setup(m => m.With).Use<LastNameSource>()
                     .Setup(m => m.Date).Use<DateOfBirthSource>();
                });

            // Generate one of these per test (factory will be a static variable most likely)
            var session = factory.CreateSession();

            // Get a collection of users
            return session.List<Contact>(10).Get();
        }
    }

    /// <summary>
    /// Data source for appointments
    /// </summary>
    public class AppointmentDataSource : DatasourceBase<List<Appointment>>
    {
        public override List<Appointment> Next(IGenerationSession session)
        {
            var dataSource = new List<Appointment>();
            for (var i = 0; i < 9; i++)
                dataSource.Add(session.Single<Appointment>().Get());
            return dataSource;
        }
    }

    /// <summary>
    /// Data source for appointments
    /// </summary>
    public class AddressNumberSource : DatasourceBase<string>
    {
        readonly Random _random = new Random();
        public override string Next(IGenerationSession session)
        {
            return this._random.Next(0, 9999).ToString("0000");
        }
    }

    /// <summary>
    /// Data source for Address line 1
    /// </summary>
    public class AddressLine1Source : DatasourceBase<string>
    {
        public override string Next(IGenerationSession session)
        {
            return new AddressNumberSource().Next(session) + " " + new FirstNameSource().Next(session) + " " + new LastNameSource().Next(session) + " Rd";
        }
    }
}