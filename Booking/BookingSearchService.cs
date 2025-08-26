namespace Airport_Ticket_Booking_System.Bookings;

public class BookingSearchService
{
    private BookingSearchQuery _criteria = new BookingSearchQuery();
    private CriteriaUtilityService _criteriaUtilityService;

    public BookingSearchService()
    {
        _criteriaUtilityService = new CriteriaUtilityService(_criteria);
    }

    public List<Booking> SearchBookings(List<Booking> bookings)
    {
        while (true)
        {
            _criteriaUtilityService.ShowCriteriaMenu();
            bool confirmed = _criteriaUtilityService.HandleCriteriaMenuInput();

            var filteredBookings = FilterBookings(bookings);

            if (filteredBookings.Count == 0)
            {
                Console.WriteLine("\nThere are no bookings matching your filters.");
                return new List<Booking>();
            }

            Console.WriteLine("\nCurrent Bookings:");
            for (int i = 0; i < filteredBookings.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {filteredBookings[i].ToDetailedString()}");
            }

            if (confirmed)
                return filteredBookings;

            Console.WriteLine("\nPress any key to continue filtering...");
            Console.ReadKey();
        }
    }

    private List<Booking> FilterBookings(List<Booking> bookings)
    {
        IEnumerable<Booking> query = bookings;

        if (!string.IsNullOrWhiteSpace(_criteria.FlightNumber))
        {
            query = query.Where(b => b.Flight.FlightNumber.Equals(_criteria.FlightNumber, StringComparison.OrdinalIgnoreCase));
        }

        if (_criteria.MaxPrice.HasValue)
        {
            query = query.Where(b => b.Flight.PricePerClass.Values.Any(price => price <= _criteria.MaxPrice.Value));
        }

        if (!string.IsNullOrWhiteSpace(_criteria.DepartureCountry))
        {
            query = query.Where(b => b.Flight.DepartureCountry.Equals(_criteria.DepartureCountry, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(_criteria.DepartureAirport))
        {
            query = query.Where(b => b.Flight.DepartureAirport.Equals(_criteria.DepartureAirport, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(_criteria.DestinationCountry))
        {
            query = query.Where(b => b.Flight.DestinationCountry.Equals(_criteria.DestinationCountry, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(_criteria.DestinationAirport))
        {
            query = query.Where(b => b.Flight.DestinationAirport.Equals(_criteria.DestinationAirport, StringComparison.OrdinalIgnoreCase));
        }

        if (_criteria.DepartureDate.HasValue)
        {
            query = query.Where(b => b.Flight.DepartureDateTime.Date == _criteria.DepartureDate.Value.Date);
        }

        if (_criteria.FlightClass.HasValue)
        {
            query = query.Where(b => b.Class == _criteria.FlightClass.Value);
        }

        return query.ToList();
    }
}
