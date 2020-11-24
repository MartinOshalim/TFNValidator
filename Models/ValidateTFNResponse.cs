using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabGroup_Task.Models
{
    public class ValidateTFNResponse
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }
}
