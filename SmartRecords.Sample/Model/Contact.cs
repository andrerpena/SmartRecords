using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRecords.Sample.Model
{
    public class Contact
    {
        public Contact()
        {
            this.Address = new Address();
            this.Appointments = new List<Appointment>();
        }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "E-mail address")]
        public string Email { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Address")]
        public Address Address { get; set; }

        [Display(Name = "Appointments")]
        public List<Appointment> Appointments { get; set; }
    }
}
