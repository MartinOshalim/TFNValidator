using LabGroup_Task.Enums;
using LabGroup_Task.Services.Logger;
using LabGroup_Task.Services.Logger.Interface;
using LabGroup_Task.Services.Options;
using LabGroup_Task.Services.Validators.Interface;

namespace LabGroup_Task.Services.Validators
{
    public class ValidatorFactory
    {
        private readonly ILogger _logger;
        private readonly RunOptions _runOptions;

        public ValidatorFactory(ILogger logger, RunOptions runOptions)
        {
            _logger = logger;
            _runOptions = runOptions;
        }

        public ITaxFileValidator CreateValidator()
        {
            var result = _runOptions?.TFNValidator ?? 0;

            switch (result)
            {
                case 0:
                    _logger.Log(LogType.Debug, $"Creating TFN validator based on appsetting configuration.");
                    return new TFNValidator(_logger);
                case 1:
                    _logger.Log(LogType.Debug, $"Creating always true validator based on appsetting configuration.");
                    return new AlwaysTrueValidator(_logger);
                case 2:
                    _logger.Log(LogType.Debug, $"Creating always false validator based on appsetting configuration.");
                    return new AlwaysFalseValidator(_logger);
                default:
                    _logger.Log(LogType.Debug, $"Creating always false validator based on appsetting configuration.");
                    return new AlwaysFalseValidator(_logger);
            }
        }
    }
}
