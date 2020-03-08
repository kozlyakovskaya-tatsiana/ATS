using AutoTelephoneStation.OperatorsATS.Models;
using AutoTelephoneStation.TelephoneStation.Models;

namespace AutoTelephoneStation.CreatingObjects
{
    public static class Builder
    {
        public static Tariff Tariff => new Tariff("Light", 12.56, 0.015);

        public static ATS ATS => new ATS();

        public static Subscriber GetSubscriber(string name, string surname) => new Subscriber(name, surname);
    }
}
