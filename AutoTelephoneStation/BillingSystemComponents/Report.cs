using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoTelephoneStation.BillingSystemComponents
{
    public class Report
    {
        public List<ReportRecord> ReportRecords { get; private set; }

        private bool IsEmpty => ReportRecords.Count == 0;

        public Report()
        {
            ReportRecords = new List<ReportRecord>();
        }

        public Report(IEnumerable<ReportRecord> records)
        {
            ReportRecords = new List<ReportRecord>(records);
        }

        public void AddRecord(ReportRecord record)
        {
            ReportRecords.Add(record);
        }

        public override string ToString()
        {
            return IsEmpty ? "No any records" : String.Join(Environment.NewLine, ReportRecords);
        }

    }
}
