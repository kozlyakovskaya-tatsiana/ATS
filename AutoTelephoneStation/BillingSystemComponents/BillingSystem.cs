using AutoTelephoneStation.Enums;
using AutoTelephoneStation.OperatorsATS.Models;
using AutoTelephoneStation.TelephoneStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoTelephoneStation.BillingSystemComponents
{
    public class BillingSystem
    {
        public ATS ATS { get; set; }

        private List<CallInformation> _callInformationStorage;

        // Key-TelephoneNumber, Value - Contract. 
        public Dictionary<int, Contract> UsersData { get; private set; }

        public BillingSystem(ATS ats)
        {
            ATS = ats;
            _callInformationStorage = new List<CallInformation>();
            UsersData = new Dictionary<int, Contract>();

            // Billing system subscribes to get info about calls from ats.
            ATS.CallInfoEvent += OnCallFromATSExecute;
        }

        public Report GetReport(int number)
        {
            var callsOfPort = _callInformationStorage.Where(record => record.FromNumber.Equals(number) || record.ToNumber.Equals(number));
            Report report = new Report();
            CallType callType;
            int tempNumber;
            double cost;
            foreach (var call in callsOfPort)
            {
                if (call.FromNumber.Equals(number))
                {
                    callType = CallType.Outgoing;
                    tempNumber = call.ToNumber;
                    cost = call.Duration.TotalSeconds * UsersData[number].Tariff.CostOfCallPerSecond;
                }
                else
                {
                    callType = CallType.Incoming;
                    tempNumber = call.FromNumber;
                    cost = 0;
                }
                report.AddRecord(new ReportRecord(callType, tempNumber, call.BeginCall, call.Duration, cost));
            }
            return report;
        }
   
        public Report GetFilteredReportBy(int number, ReportFilter reportFilter)
        {
            switch (reportFilter)
            {
                case ReportFilter.Date:
                    return new Report(GetReport(number).ReportRecords.OrderBy(record => record.DateOfCall));
                case ReportFilter.Cost:
                    return new Report(GetReport(number).ReportRecords.OrderBy(record => record.Cost));
                case ReportFilter.Incoming:
                    return new Report(GetReport(number).ReportRecords.Where(record => record.CallType.Equals(CallType.Incoming)));
                case ReportFilter.Outgoing:
                    return new Report(GetReport(number).ReportRecords.Where(record => record.CallType.Equals(CallType.Outgoing)));
                default:
                    throw new Exception("This filter is not available");
            }
        }

        public Report GetFilterReportBySubscriber(int number, int numberToFilterBy)
        {
            return new Report(GetReport(number).ReportRecords.Where(record => record.NumberToCommunicate.Equals(numberToFilterBy)));
        }

        public void OnRegisterUserExecute(object sender, Contract contract)
        {
            UsersData.Add(contract.TelephoneNumber, contract);
        }

        private void OnCallFromATSExecute(object sender, CallInformation callInformation)
        {
            _callInformationStorage.Add(callInformation);
        }

        public string GetAllInfo()
        {
            return String.Join(Environment.NewLine, _callInformationStorage);
        }
    }
}
