using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabGroup_Task.Services.Validators.Interface
{
    public interface ITaxFileValidator
    {
        public string TaxFileNumber { get; set; }
        public bool IsValid();
        public bool ProcessValidationAlgorithm();

    }
}
