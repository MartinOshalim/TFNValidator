using LabGroup_Task.Enums;
using LabGroup_Task.Services.Logger.Interface;
using LabGroup_Task.Services.Validators.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabGroup_Task.Services.Validators
{
    public abstract class ValidatorBase : ITaxFileValidator
    {
        protected ILogger _logger;
        public string TaxFileNumber { get; set; }
        public ValidatorBase(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Method that will contain the business logic to validate, abstract since each derived will have its own logic.
        /// </summary>
        /// <returns></returns>
        public abstract bool ProcessValidationAlgorithm();

        /// <summary>
        /// Method called to Log and Process the TFN to check its valid.
        /// </summary>
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(TaxFileNumber))
            {
                _logger.Log(LogType.Error, $"No TFN was set: {TaxFileNumber}");
                return false;
            }

            _logger.Log(LogType.Debug, $"Validating TFN: {TaxFileNumber}");
            var result = ProcessValidationAlgorithm();
            _logger.Log(LogType.Debug, $"Result from TFN validation: {result}");

            return result;
        }

    }
}
