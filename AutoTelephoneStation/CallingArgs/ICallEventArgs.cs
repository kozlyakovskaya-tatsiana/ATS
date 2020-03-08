using AutoTelephoneStation.TelephoneStation.Models;
using System;

namespace AutoTelephoneStation.CallingArgs
{
    public interface ICallEventArgs
    {
        int FromTelephoneNumber { get; }

        int ToTelephoneNumber { get; }

        DateTime BeginCall { get;  }

        DateTime EndCall { get;  }

        TimeSpan Duration { get; }
    }
}
