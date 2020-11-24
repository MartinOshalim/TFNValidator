using LabGroup_Task.Enums;
using LabGroup_Task.Services.Logger.Interface;
using LabGroup_Task.Services.Validators.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabGroup_Task.Services.Validators
{
    public class TFNValidator : ValidatorBase
    {

        /// <summary>
        /// Australian TFN Validator
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="tfnNumber"></param>
        public TFNValidator(ILogger logger) : base(logger)
        {

        }

        /// <summary>
        /// The Australian TFN Algorithm
        /// </summary>
        /// <returns>true/false</returns>
        public override bool ProcessValidationAlgorithm()
        {
            var weightingFactor = new List<int>();

            if (TaxFileNumber.Length == 8)
            {
                weightingFactor = new List<int>()
                {
                    10,7,8,4,6,3,5,1
                };
            }
            else
            {
                weightingFactor = new List<int>()
                {
                    10,7,8,4,6,3,5,2,1
                };
            }

            try
            {
                var totalRunningSum = 0;

                //Multiply each number against corresponding weight.
                for (int i = 0; i < TaxFileNumber.Length; i++)
                {
                    int.TryParse(TaxFileNumber[i].ToString(), out var result);
                    totalRunningSum += result * weightingFactor[i];
                }

                //divide by 11 and ensure remainder is equal to 0
                var equalToZero = totalRunningSum % 11 == 0;
                
                return equalToZero;

            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, $"An error occured during TFN validation: {e.Message}.");
                return false;
            }
        }
    }
}
