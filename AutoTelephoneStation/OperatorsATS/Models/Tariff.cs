
namespace AutoTelephoneStation.OperatorsATS.Models
{
    public class Tariff
    {
        public string Name { get; private set; }

        public double CostOfMonth { get; private set; }

        public double CostOfCallPerSecond { get; private set; }

        public Tariff(string name, double costOfMonth, double costOfCallPerSecond)
        {
            Name = name;
            CostOfMonth = costOfMonth;
            CostOfCallPerSecond = costOfCallPerSecond;
        }
    }
}
