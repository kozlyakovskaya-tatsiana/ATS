using AutoTelephoneStation.CallingArgs;
using AutoTelephoneStation.CallingArgs.Models;
using AutoTelephoneStation.Enums;
using AutoTelephoneStation.OperatorsATS.Models;
using System;
using System.Collections.Generic;

namespace AutoTelephoneStation.TelephoneStation.Models
{
    public class ATS 
    {
        public event EventHandler<CallInformation> CallInfoEvent;

        // Key - telephoneNumber, Port - Port of telephoneNumber.
        private Dictionary<int, Port> _numberPortStorage;

        public ATS()
        {
            _numberPortStorage = new Dictionary<int, Port>();
        }

        private DateTime _startConversation;

        private DateTime _endConversation;

        public void OnRegisterUserExecute(object sender, Contract contract)
        {
            _numberPortStorage.Add(contract.TelephoneNumber, contract.Port);
            contract.Port.CallEvent += OnCallExecute;
            contract.Port.AnswerEvent += OnAnswerExecute;
            contract.Port.EndCallevent += OnEndCallExecute;
            contract.Port.DeclineEvent += OnDeclineExecute;
        }

        public void OnCallExecute(object obj, ICallEventArgs args)
        {
            Console.WriteLine($"ATS: {args.FromTelephoneNumber} calls to  {args.ToTelephoneNumber}");
            Console.WriteLine($"ATS: Connection {args.FromTelephoneNumber} with {args.ToTelephoneNumber}");

            if (!_numberPortStorage.ContainsKey(args.ToTelephoneNumber))
            {
                var date = DateTime.Now;

                Console.WriteLine($"ATS: Connection is not available. There is no {args.ToTelephoneNumber} in the system");
                _numberPortStorage[args.FromTelephoneNumber].GetResponseFromATS("Connection is not available. There is no such number in the system");

                CallInfoEvent?.Invoke(this, new CallInformation(args.FromTelephoneNumber, args.ToTelephoneNumber, date, date));

                return;
            }
            if (args.FromTelephoneNumber.Equals(args.ToTelephoneNumber))
            {
                _numberPortStorage[args.FromTelephoneNumber].GetResponseFromATS("It is impossible to call itself");

                return;
            }
            
            if (_numberPortStorage[args.ToTelephoneNumber].IsAvailable)
            {
                _numberPortStorage[args.ToTelephoneNumber].HaveIncomingCall(args.FromTelephoneNumber);
            }
            else
            {
                var dateTime = DateTime.Now;

                Console.WriteLine($"ATS: {args.ToTelephoneNumber} is not available now");
                _numberPortStorage[args.FromTelephoneNumber].GetResponseFromATS($"{args.ToTelephoneNumber} is not available now");

                CallInfoEvent?.Invoke(this, new CallInformation(args.FromTelephoneNumber, args.ToTelephoneNumber, dateTime, dateTime));
            }
        }

        public void OnAnswerExecute(object obj, ICallEventArgs args)
        {
            _startConversation = DateTime.Now;

            _numberPortStorage[args.FromTelephoneNumber].ChangePortState(PortState.Calling);
            _numberPortStorage[args.ToTelephoneNumber].ChangePortState(PortState.Calling);

            Console.WriteLine($"ATS: {args.ToTelephoneNumber} talks with {args.FromTelephoneNumber} {DateTime.Now}");
        }

        public void OnDeclineExecute(object obj, ICallEventArgs args)
        {
            var dateTime = DateTime.Now;

            Console.WriteLine($"ATS: {args.ToTelephoneNumber} rejected call from {args.FromTelephoneNumber}");
            _numberPortStorage[args.FromTelephoneNumber].GetResponseFromATS($"{args.ToTelephoneNumber} rejected your call");


            CallInfoEvent?.Invoke(this, new CallInformation(args.FromTelephoneNumber, args.ToTelephoneNumber, dateTime, dateTime));
        }

        public void OnEndCallExecute(object sender, ICallEventArgs args)
        {
            _endConversation = DateTime.Now;

            Console.WriteLine($"ATS: {args.ToTelephoneNumber} finished talk with {args.FromTelephoneNumber} {DateTime.Now}");

            _numberPortStorage[args.FromTelephoneNumber].GetResponseFromATS($"finished talk with {args.ToTelephoneNumber}");
            _numberPortStorage[args.ToTelephoneNumber].GetResponseFromATS($"finished talk with {args.FromTelephoneNumber}");

            _numberPortStorage[args.FromTelephoneNumber].ChangePortState(PortState.Connected);
            _numberPortStorage[args.ToTelephoneNumber].ChangePortState(PortState.Connected);

            CallInfoEvent?.Invoke(this, new CallInformation(args.FromTelephoneNumber, args.ToTelephoneNumber, _startConversation, _endConversation));
        }
    }

}
