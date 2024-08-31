#region Using directives
using System;
using CoreBase = FTOptix.CoreBase;
using FTOptix.HMIProject;
using UAManagedCore;
using FTOptix.UI;
using FTOptix.NetLogic;
using FTOptix.Modbus;
using FTOptix.System;
#endregion

public class KE_ClockLogic : BaseNetLogic
{
    private PeriodicTask periodicTask;

    public override void Start()
    {
        periodicTask = new PeriodicTask(UpdateTime, 1000, LogicObject);
        periodicTask.Start();
    }

    public override void Stop()
    {
        periodicTask.Dispose();
        periodicTask = null;
    }

    private void UpdateTime()
    {
        if (LogicObject != null)
        {
            LogicObject.GetVariable("Time").Value = DateTime.Now;
            LogicObject.GetVariable("UTCTime").Value = DateTime.UtcNow;

            // Lynch - Check if it's midnight and set a variable
            bool isMidnight = IsMidnightNow();
            LogicObject.GetVariable("IsMidnight").Value = isMidnight;

            // Lynch - create methods for 1st 2nd and 3rd start times
            bool isFirstNow = IsFirstNow();
            LogicObject.GetVariable("Start1stShift").Value = isFirstNow;

            bool isSecondNow = IsSecondNow();
            LogicObject.GetVariable("Start2ndShift").Value = isSecondNow;

            bool isThirdNow = IsThirdNow();
            LogicObject.GetVariable("Start3rdShift").Value = isThirdNow;

        }
    }

    private bool IsMidnightNow()
    {
        // Get the current time
        DateTime currentTime = DateTime.Now;

        // Check if it's midnight (00:00)
        return currentTime.Hour == 0 && currentTime.Minute == 0;
    }

    private bool IsFirstNow()
    {
        if (LogicObject != null)
        {
            // Grab start hour and minute from Properties in Optix for the first shift
            int h = LogicObject.GetVariable("Start1stShift/StartHR").Value;
            int m = LogicObject.GetVariable("Start1stShift/StartMin").Value;

            // Get the current time
            DateTime currentTime = DateTime.Now;

            // Check if it's the specified time. The seconds can be add see next line
            return currentTime.Hour == h && currentTime.Minute == m; ;//&& currentTime.Second == 0;
        }

        // Handle the case where LogicObject is null or properties are not found.
        return false;
    }

    private bool IsSecondNow()
    {
        if (LogicObject != null)
        {
            // Grab start hour and minute from Properties in Optix for the second shift
            int h = LogicObject.GetVariable("Start2ndShift/StartHR").Value;
            int m = LogicObject.GetVariable("Start2ndShift/StartMin").Value;

            // Get the current time
            DateTime currentTime = DateTime.Now;

            // Check if it's the specified time. The seconds can be add see next line
            return currentTime.Hour == h && currentTime.Minute == m; ;//&& currentTime.Second == 0;
        }

        // Handle the case where LogicObject is null or properties are not found.
        return false;
    }
    private bool IsThirdNow()
    {
        if (LogicObject != null)
        {
            // Grab start hour and minute from Properties in Optix for the second shift
            int h = LogicObject.GetVariable("Start3rdShift/StartHR").Value;
            int m = LogicObject.GetVariable("Start3rdShift/StartMin").Value;

            // Get the current time
            DateTime currentTime = DateTime.Now;

            // Check if it's the specified time. The seconds can be add see next line
            return currentTime.Hour == h && currentTime.Minute == m; ;//&& currentTime.Second == 0;
        }

        // Handle the case where LogicObject is null or properties are not found.
        return false;
    }
}

