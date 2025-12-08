using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsEvnetLog
    {
        public static void Eventlog(string massega)
        {
            string DVLDApp = "DVLD";

            if (!EventLog.Exists(DVLDApp))
            {
                EventLog.CreateEventSource(DVLDApp, "Application");
                Console.WriteLine("Event Source Create");
            }

            EventLog.WriteEntry(DVLDApp, massega, EventLogEntryType.Error);
        }
    }
}
