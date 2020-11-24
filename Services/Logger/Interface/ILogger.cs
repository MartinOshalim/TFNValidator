using LabGroup_Task.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabGroup_Task.Services.Logger.Interface
{
    public interface ILogger
    {
        void Log(LogType logType, string errorMessage);
    }
}
