using AutoTelephoneStation.Enums;
using System;

namespace AutoTelephoneStation.BillingSystemComponents
{
    public class ReportRecord
    {
        public CallType CallType { get; private set; }

        public int NumberToCommunicate { get; private set; }

        public DateTime DateOfCall { get; private set; }

        public TimeSpan Duration { get; private set; }

        public double Cost { get; private set; }

        public ReportRecord(CallType callType, int numberToCommunicate, DateTime date, TimeSpan duration, double cost)
        {
            CallType = callType;
            NumberToCommunicate = numberToCommunicate;
            DateOfCall = date;
            Duration = duration;
            Cost = cost;
        }

        public override string ToString()
        {
            return $"TelephoneNumber: {NumberToCommunicate} CallType: {CallType} Date: {DateOfCall} Duration: {Duration}  Cost: {Cost} rubles";
        }
    }
}
