using LabGroup_Task.Enums;
using LabGroup_Task.Services.Logger.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabGroup_Task.Services.LinkedProcesser
{
    public class LinkedProcesser
    {
        ILogger _logger;
        private List<string> _patternList;
        public LinkedProcesser(ILogger logger)
        {
            _logger = logger;
            _patternList = new List<string>();
        }
       
        public List<string> GetAllUniquePatterns(string pattern)
        {
            if(string.IsNullOrEmpty(pattern))
            {
                _logger.Log(LogType.Error, $"Empty pattern passed to linked processer :{pattern}");
                return _patternList;
            }
            
            for(int i = 0; i < pattern.Length-3; i++)
            {
                _patternList.Add(pattern.Substring(i, 4));
            }

            //Return distinct patterns i.e dont create a list of 1111, 1111.
            return _patternList.Distinct().ToList();
        }
    }
}
