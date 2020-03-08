
namespace AutoTelephoneStation.OperatorsATS.Models
{
    public class Contract
    {
        public Port Port { get; private set; }

        public int TelephoneNumber => Port.TelephoneNumber;

        public Terminal Terminal { get; private set; }

        public Tariff Tariff { get; private set; }

        public Subscriber Subscriber { get; private set; }

        public Contract(Port port, Terminal terminal, Subscriber subscriber, Tariff tarrif)
        {
            Port = port;
            Terminal = terminal;
            Subscriber = subscriber;
            Tariff = tarrif;
        }

        public override bool Equals(object obj)
        {
            return obj is Contract && TelephoneNumber.Equals(((Contract)obj).TelephoneNumber);
        }

        public override int GetHashCode()
        {
            return TelephoneNumber.GetHashCode();
        }
    }
}
