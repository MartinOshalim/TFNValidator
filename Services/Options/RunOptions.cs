using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabGroup_Task.Services.Options
{
    public class RunOptions
    {
        //TFN Validator to use 0 for Australian TFN, 1 for always True, 2 for always False
        public int TFNValidator { get; set; }
        
        // Number of seconds to store cached values, if not provided default is 30 seconds.
        public int CacheSeconds { get; set; }

        // The number of patterns required to link a detection, If not  provided its set to 3.
        public int MaxRequiredCacheDetections { get; set; }
    }
}
