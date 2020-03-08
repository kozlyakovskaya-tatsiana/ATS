using System;

namespace AutoTelephoneStation.TelephoneStation.Models
{
    public class CallInformation
    {
        public Guid Guid { get; private set; }

        public int FromNumber { get; private set; }

        public int ToNumber { get; private set; }

        public DateTime BeginCall { get; private set; }

        public DateTime EndCall { get; private set; }

        public TimeSpan Duration => EndCall.Subtract(BeginCall).Duration();

        public CallInformation(int fromNumber, int toNumber, DateTime beginCall, DateTime endCall)
        {
            Guid = Guid.NewGuid();
            FromNumber = fromNumber;
            ToNumber = toNumber;
            BeginCall = beginCall;
            EndCall = endCall;
        }

        public override string ToString()
        {
            return $"{FromNumber} called to {ToNumber} {BeginCall}-{EndCall} Duration = {Duration}";
        }
    }
}
