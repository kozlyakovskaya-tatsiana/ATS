using AutoTelephoneStation.CallingArgs;
using AutoTelephoneStation.Enums;
using System;

namespace AutoTelephoneStation.OperatorsATS.Models
{
    public class Terminal
    {
        public Guid Guid { get; private set; }

        public Port Port { get; private set; }

        public Terminal(Port port)
        {
            Guid = Guid.NewGuid();
            Port = port;
            Port.ChangePortStateEvent += OnChangeStateExecute;
        }

        public void ConnectPort()
        {
            if (Port.PortState == PortState.Disconnected)
            {
                Port.ChangePortState(PortState.Connected);
                Port.IncomingCallEvent += OnIncomeCall;
                Port.GetResponseFromATSEvent += OnResponseATS;
                Port.AnswerEvent += OnAnswer;
            }
        }

        public void DisconnectPort()
        {
            if (!(Port.PortState == PortState.Disconnected))
            {
                Port.ChangePortState(PortState.Disconnected);
                Port.IncomingCallEvent -= OnIncomeCall;
                Port.GetResponseFromATSEvent -= OnResponseATS;
                Port.AnswerEvent -= OnAnswer;
            }
        }

        public void Call(int numberToCall)
        {
            if (Port.IsAvailable)
                Port.Call(numberToCall);   
        }

        private void OnChangeStateExecute()
        {
            Console.WriteLine($"{Port.TelephoneNumber}: portstate is {Port.PortState}");
        }

        private void OnIncomeCall(object sender, ICallEventArgs args)
        {
            Console.WriteLine($"{Port.TelephoneNumber}: Incoming call from {args.FromTelephoneNumber}");
            Console.WriteLine("Answer?(Y -- yes, another symbol -- no)");
            char choise = Console.ReadKey(true).KeyChar;
            if (choise.Equals('Y'))
                Port.Answer(args.FromTelephoneNumber);
            else
                Port.Decline(args.FromTelephoneNumber);
        }

        private void OnResponseATS(object sender, IResponseFromATSargs args)
        {
            Console.WriteLine($"{Port.TelephoneNumber}: " + args.MessageResponse);
        }

        private void OnAnswer(object sender, ICallEventArgs args)
        {
            Console.WriteLine($"{Port.TelephoneNumber}: talking with {args.FromTelephoneNumber}");
            Console.WriteLine($"{Port.TelephoneNumber}: Press any key to break conversation with {args.FromTelephoneNumber}");
            Console.ReadKey(true);
           // Port.EndCall(args.FromTelephoneNumber);
        }

    }
}
