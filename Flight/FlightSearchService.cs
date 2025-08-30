namespace Airport_Ticket_Booking_System.Flights;

public class FlightSearchService
{
    private FlightSearchQuery _criteria = new FlightSearchQuery();
    private CriteriaUtilityService _criteriaUtilityService;

    public FlightSearchService()
    {
        _criteriaUtilityService = new CriteriaUtilityService(_criteria);
    }

    public List<Flight> SearchFlights(List<Flight> flights)
    {
        while (true)
        {
            _criteriaUtilityService.ShowCriteriaMenu();
            bool confirmed = _criteriaUtilityService.HandleCriteriaMenuInput();

            var filteredFlights = FilterFlights(flights);

            if (filteredFlights.Count == 0)
            {
                Console.WriteLine("\nThere are no flights matching your filters.");
                return new List<Flight>();
            }

            Console.WriteLine("\nAvailable Flights:");
            for (int i = 0; i < filteredFlights.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {filteredFlights[i].ToDetailedString()}");
            }

            if (confirmed)
                return filteredFlights;

            Console.WriteLine("\nPress any key to continue filtering...");
            Console.ReadKey();
        }
    }

    private List<Flight> FilterFlights(List<Flight> flights)
    {
        IEnumerable<Flight> query = flights;

        if (_criteria.MaxPrice.HasValue)
        {
            query = query.Where(f => f.PricePerClass.Values.Any(p => p <= _criteria.MaxPrice.Value));
        }

        if (!string.IsNullOrWhiteSpace(_criteria.DepartureCountry))
        {
            query = query.Where(f => f.DepartureCountry.Equals(_criteria.DepartureCountry, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(_criteria.DepartureAirport))
        {
            query = query.Where(f => f.DepartureAirport.Equals(_criteria.DepartureAirport, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(_criteria.DestinationCountry))
        {
            query = query.Where(f => f.DestinationCountry.Equals(_criteria.DestinationCountry, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(_criteria.DestinationAirport))
        {
            query = query.Where(f => f.DestinationAirport.Equals(_criteria.DestinationAirport, StringComparison.OrdinalIgnoreCase));
        }

        if (_criteria.DepartureDate.HasValue)
        {
            query = query.Where(f => f.DepartureDateTime.Date == _criteria.DepartureDate.Value.Date);
        }

        return query.ToList();
    }
}
