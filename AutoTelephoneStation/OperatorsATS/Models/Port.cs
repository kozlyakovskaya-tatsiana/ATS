using AutoTelephoneStation.CallingArgs;
using AutoTelephoneStation.CallingArgs.Models;
using AutoTelephoneStation.Enums;
using System;

namespace AutoTelephoneStation.OperatorsATS.Models
{
    public class Port
    {
        public event EventHandler<ICallEventArgs> CallEvent;

        public event EventHandler<ICallEventArgs> IncomingCallEvent;

        public event EventHandler<IResponseFromATSargs> GetResponseFromATSEvent;

        public event EventHandler<ICallEventArgs> AnswerEvent;

        public event EventHandler<ICallEventArgs> DeclineEvent;

        public event EventHandler<ICallEventArgs> EndCallevent;

        public event Action ChangePortStateEvent;

        public Guid Id { get; private set; }

        public int TelephoneNumber { get; private set; }

        public PortState PortState { get; private set; }

        public bool IsAvailable => PortState == PortState.Connected;

        public Port(int telephoneNumber)
        {
            Id = Guid.NewGuid();
            TelephoneNumber = telephoneNumber;
            PortState = PortState.Disconnected;
        }

        public void ChangePortState(PortState portState)
        {
            PortState = portState;
            ChangePortStateEvent?.Invoke();
        }

        public void Call(int numberToCall)
        {
            CallEvent?.Invoke(this, new CallEventArgs(TelephoneNumber, numberToCall));
        }

        public void HaveIncomingCall(int fromNumber)
        {
            IncomingCallEvent?.Invoke(this, new CallEventArgs(fromNumber, TelephoneNumber));
        }

        public void GetResponseFromATS(string message)
        {
            GetResponseFromATSEvent?.Invoke(this, new ResponseFromATSEventArgs(message));
        }

        public void Decline(int fromNumber)
        {
            DeclineEvent?.Invoke(this, new CallEventArgs(fromNumber, TelephoneNumber));
        }

        public void Answer(int fromNumber)
        {
            AnswerEvent?.Invoke(this, new CallEventArgs(fromNumber, TelephoneNumber));
        }

        public void EndCall(int withNumber)
        {
            EndCallevent?.Invoke(this, new CallEventArgs(withNumber, TelephoneNumber));
        }

        public override int GetHashCode()
        {
            return TelephoneNumber.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Port && TelephoneNumber.Equals(((Port)obj).TelephoneNumber);
        }

    }
}