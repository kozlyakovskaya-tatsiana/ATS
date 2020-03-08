using AutoTelephoneStation.BillingSystemComponents;
using System;

namespace AutoTelephoneStation.OperatorsATS.Models
{
    public class Company
    {
        private static Random _random = new Random();

        private BillingSystem _billingSystem;

        public event EventHandler<Contract> RegisterUserEvent;

        public Company(BillingSystem billingSystem)
        {
            _billingSystem = billingSystem;

            // Billing system subscribes on user registration. 
            RegisterUserEvent += _billingSystem.OnRegisterUserExecute;

            // ATS of billing system subscribes on user registration. 
            RegisterUserEvent += _billingSystem.ATS.OnRegisterUserExecute;
        }

        public Contract RegisterContract(Subscriber subscriber, Tariff tariff)
        {
            int telephoneNumber;
            do
            {
                telephoneNumber = _random.Next(1000000, 9999999);
            } while (_billingSystem.UsersData.ContainsKey(telephoneNumber));

            var port = new Port(telephoneNumber);
            var terminal = new Terminal(port);
            var contract = new Contract(port, terminal, subscriber, tariff);

            RegisterUserEvent?.Invoke(this, contract);

            return contract;
        }

        public Terminal GetTerminal(Contract contract)
        {
            return contract.Terminal;
        }
    }
}
