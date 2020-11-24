using LabGroup_Task.Services.Logger.Interface;
using LabGroup_Task.Services.Validators.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabGroup_Task.Services.Validators
{
    public class AlwaysFalseValidator : ValidatorBase
    {

        /// <summary>
        /// Mock always false validator, for testing purposes.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="tfnNumber"></param>
        public AlwaysFalseValidator(ILogger logger) : base(logger)
        {

        }

        /// <summary>
        /// Validation algorithm that always returns false.
        /// </summary>
        /// <returns>false</returns>
        public override bool ProcessValidationAlgorithm()
        {
            return false;
        }
    }
}
