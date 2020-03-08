using System;
using AutoTelephoneStation.CreatingObjects;
using AutoTelephoneStation.BillingSystemComponents;
using AutoTelephoneStation.OperatorsATS.Models;
using AutoTelephoneStation.Enums;

namespace AutoTelephoneStation
{
    class Program
    {
        static void Main(string[] args)
        {
            var ats = Builder.ATS;
            var billingSystem = new BillingSystem(ats);
            var company = new Company(billingSystem);

            var tariff = Builder.Tariff;

            var subscriber1 = Builder.GetSubscriber("Tanya", "Kozlyakovskaya");
            var subscriber2 = Builder.GetSubscriber("Valera", "Zolotov");
            var subscriber3 = Builder.GetSubscriber("Maxim", "Kotov");
            var subscriber4 = Builder.GetSubscriber("Nastya", "Polivoda");

            var terminal1 = company.GetTerminal(company.RegisterContract(subscriber1, tariff));
            var terminal2 = company.GetTerminal(company.RegisterContract(subscriber2, tariff));
            var terminal3 = company.GetTerminal(company.RegisterContract(subscriber3, tariff));
            var terminal4 = company.GetTerminal(company.RegisterContract(subscriber4, tariff));

            terminal1.ConnectPort();
            terminal2.ConnectPort();
            terminal3.ConnectPort();
            terminal4.ConnectPort();

            // Call from disconnected terminal
            terminal4.Call(terminal1.Port.TelephoneNumber);
            Console.WriteLine("--------------------------");

            // Proper call. (Answer to a call).
            terminal1.Call(terminal2.Port.TelephoneNumber);
            Console.WriteLine("--------------------------");

            terminal3.Call(terminal4.Port.TelephoneNumber);

            // Proper call. (Decline a call).
            terminal2.Call(terminal1.Port.TelephoneNumber);
            Console.WriteLine("--------------------------");

            // Call from proper terminal to disconnected.
            terminal1.Call(terminal4.Port.TelephoneNumber);
            Console.WriteLine("--------------------------");

            // Proper terminal calls itself.
            terminal3.Call(terminal3.Port.TelephoneNumber);
            Console.WriteLine("--------------------------");

            // Proper call. (Answer to a call).
            terminal1.Call(terminal2.Port.TelephoneNumber);
            Console.WriteLine("--------------------------");

            // Proper call. (Answer to a call).
            terminal1.Call(terminal2.Port.TelephoneNumber);
            Console.WriteLine("--------------------------");

            Console.WriteLine("==========================");
            Console.WriteLine($"All information about calls\n{billingSystem.GetAllInfo()}");
            Console.WriteLine("==========================");

            Console.WriteLine($"Information about calls for {terminal1.Port.TelephoneNumber}:\n{billingSystem.GetReport(terminal1.Port.TelephoneNumber)}");
            Console.WriteLine("==========================");

            Console.WriteLine($"Information about incoming calls for {terminal1.Port.TelephoneNumber}:\n" +
                $"{billingSystem.GetFilteredReportBy(terminal1.Port.TelephoneNumber, ReportFilter.Incoming)}");
            Console.WriteLine("==========================");

            Console.WriteLine($"Information about calls for {terminal1.Port.TelephoneNumber} with {terminal2.Port.TelephoneNumber}:\n" +
               $"{billingSystem.GetFilterReportBySubscriber(terminal1.Port.TelephoneNumber, terminal2.Port.TelephoneNumber)}");
            Console.WriteLine("==========================");

            Console.WriteLine("The end");

            Console.ReadKey();
        }
    }
}
