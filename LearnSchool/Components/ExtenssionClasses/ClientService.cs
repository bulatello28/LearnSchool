using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnSchool.Components
{
    public partial class ClientService
    {
        public TimeSpan TimeBeforeRecordStart => StartTime.Subtract(DateTime.Now);

        public double HoursBeforeRecordStart => TimeBeforeRecordStart.Hours + TimeBeforeRecordStart.Days * 24;
        public double MinutesBeforeRecordStart => TimeBeforeRecordStart.Minutes;
    }
}
