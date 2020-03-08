using AutoTelephoneStation.TelephoneStation.Models;
using System;

namespace AutoTelephoneStation.CallingArgs.Models
{
    public class CallEventArgs : ICallEventArgs
    {
        public int FromTelephoneNumber { get; private set; }

        public int ToTelephoneNumber { get; private set; }

        public DateTime BeginCall { get; private set; }

        public DateTime EndCall { get; private set; }

        public TimeSpan Duration => EndCall.Subtract(BeginCall).Duration();

        public CallEventArgs(int fromTelephoneNumber, int toTelephoneNumber)
        {
            FromTelephoneNumber = fromTelephoneNumber;
            ToTelephoneNumber = toTelephoneNumber;
        }

    }
}
