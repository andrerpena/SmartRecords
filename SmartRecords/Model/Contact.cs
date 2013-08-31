using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Model
{
    class Contact
    {
        public Contact()
        {
            this.Address = new Address();
            this.Phones = new List<Phone>();
        }

        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Social Security Number")]
        public string SSN { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public Address Address { get; set; }

        [Display(Name = "Phones")]
        public List<Phone> Phones { get; set; }
    }
}
