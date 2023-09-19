using System.Security.AccessControl;

namespace ScooterRental
{
    public class ScooterService : IScooterService
    {
        private readonly List<Scooter> _scooters;

        public ScooterService(List<Scooter> scooterList)
        {
            _scooters = scooterList;
        }
        public void AddScooter(string id, decimal pricePerMinute)
        {       
            if (_scooters.Any(s => s.Id == id))
                throw new ScooterDuplicateException();
            
            if (pricePerMinute <= 0)
                throw new NegativePriceException();

            if (string.IsNullOrEmpty(id))
                throw new InvalidIdException();
            
            _scooters.Add(new Scooter(id, pricePerMinute));
        }

        public Scooter GetScooterById(string scooterId)
        {
            Scooter scooterToReturn = _scooters.FirstOrDefault(s => s.Id == scooterId);

            if (scooterToReturn != null)
                return _scooters.FirstOrDefault(s => s.Id == scooterId);
            else
                throw new InvalidIdException();
        }

        public IList<Scooter> GetScooters()
        {
            return _scooters.ToList();
        }

        public void RemoveScooter(string id)
        {
            Scooter scooterToRemove = _scooters.FirstOrDefault(s => s.Id == id);

            if (scooterToRemove != null)
                _scooters.Remove(scooterToRemove);
            else
                throw new InvalidIdException();
        }
    }
}
