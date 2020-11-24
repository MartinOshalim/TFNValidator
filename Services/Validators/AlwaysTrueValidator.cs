using LabGroup_Task.Services.Logger.Interface;
using LabGroup_Task.Services.Validators.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabGroup_Task.Services.Validators
{
    public class AlwaysTrueValidator : ValidatorBase
    {
        /// <summary>
        /// Mock always true validator, for testing purposes.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="tfnNumber"></param>
        public AlwaysTrueValidator(ILogger logger) : base(logger)
        {

        }

        /// <summary>
        /// Validation algorithm that always returns true.
        /// </summary>
        /// <returns>true</returns>
        public override bool ProcessValidationAlgorithm()
        {
            return true;
        }
    }
}
