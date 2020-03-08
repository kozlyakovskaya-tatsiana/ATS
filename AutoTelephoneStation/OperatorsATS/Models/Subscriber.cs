
namespace AutoTelephoneStation.OperatorsATS.Models
{
    public class Subscriber
    {
        public string Name { get; private set; }

        public string Surname { get; private set; }

        public Subscriber(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}
