using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabGroup_Task.Models
{
    public class Person
    {
        [DisplayName("First Name")]
        [StringLength(64)]

        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [StringLength(64)]

        public string LastName { get; set; }
        [DisplayName("Tax File Number (TFN)")]
        [StringLength(9, MinimumLength = 8, ErrorMessage = "Please enter 8 or 9 numeric characters only.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter 8 or 9 numeric characters only.")]
        public string TaxFileNumber { get; set; }
    }
}
