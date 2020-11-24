using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LabGroup_Task.Enums;
using LabGroup_Task.Models;
using LabGroup_Task.Services.Cache;
using LabGroup_Task.Services.LinkedProcesser;
using LabGroup_Task.Services.Logger.Interface;
using LabGroup_Task.Services.Options;
using LabGroup_Task.Services.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LabGroup_Task.Controllers
{
    public class PersonController : Controller
    {
        private readonly ILogger _logger;
        private readonly RunOptions _runOptions;
        private IMemoryCache _cache;

        public PersonController(ILogger logger, RunOptions runOptions, IMemoryCache cache)
        {
            _logger = logger;
            _runOptions = runOptions;
            _cache = cache;
        }

        // GET: PersonController
        public ActionResult Index()
        {
            return View();
        }

        // POST: PersonController
        [HttpPost]
        public ValidateTFNResponse ValidateTFN([FromBody] Person person)
        {
            // Reponse is too fast, adding this here just so I can see the spinner (not for prod).
            Thread.Sleep(2000);

            if(string.IsNullOrEmpty(person?.TaxFileNumber) || !int.TryParse(person?.TaxFileNumber, out var tfnNumber))
            {
                _logger.Log(LogType.Error, $"Invalid TFN Entered: {person?.TaxFileNumber}");
                return new ValidateTFNResponse()
                {
                    IsValid = false
                };
            }

            _logger.Log(LogType.Debug, $"Calling validator factory to create validator.");

            //Get All Patterns from the TFN entered.
            LinkedProcesser linkedProcesser = new LinkedProcesser(_logger);
            var result =  linkedProcesser.GetAllUniquePatterns(person.TaxFileNumber);

            //Check if patterns exist in cache return false i.e no valid.
            CacheStorage cacheStorage = new CacheStorage(_cache, _logger, _runOptions);
            object cacheResult = null;
            int cacheResultsFound = 0;

            foreach (var item in result)
            {
                cacheResult = cacheStorage.GetCacheEntry(item);
                if(cacheResult != null)
                {
                    cacheResultsFound++;
                    _logger.Log(LogType.Debug, $"Pattern found in cache: {cacheResult}");
                    if (cacheResultsFound == (_runOptions.MaxRequiredCacheDetections != 0 ? _runOptions.MaxRequiredCacheDetections : 3))
                    {
                        _logger.Log(LogType.Error, $"{_runOptions.MaxRequiredCacheDetections} patterns found in cache, return error message.");
                        
                        return new ValidateTFNResponse()
                        {
                            IsValid = false,
                            ErrorMessage = "Validation tool doesn't allow multiple attempts for similar values "
                        };
                    }
                }
            }

            //Otherwise nothing is found add all unique patterns in the cache.
            foreach (var item in result)
            {
                cacheStorage.AddCacheEntry(item);
            }

            // Create a validator, assign the TFN to the validator and Process it.
            ValidatorFactory validatorFactory = new ValidatorFactory(_logger, _runOptions);
            var TFNValidator = validatorFactory.CreateValidator();
            TFNValidator.TaxFileNumber = person?.TaxFileNumber;

            return new ValidateTFNResponse()
            {
                IsValid = TFNValidator.IsValid()
            };
        }


    }
}
