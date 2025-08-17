using AirportTicketBookingSystem.Handlers;

namespace Airport_Ticket_Booking_System.Flights;

public class FlightSearchService
{
    private FlightSearchCriteria _criteria = new FlightSearchCriteria();

    public List<Flight> SearchFlights(List<Flight> flights)
    {
        while (true)
        {
            ShowCriteriaMenu();
            bool confirmed = HandleCriteriaMenuInput();

            var filteredFlights = FilterFlights(flights);

            if (filteredFlights.Count == 0)
            {
                Console.Write("\nNo matching flights. Retry filtering? (y/n): ");

                if (Console.ReadLine()?.ToLower() != "y")
                {
                    return new List<Flight>();
                }

                continue;
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

    private void ShowCriteriaMenu()
    {
        Console.WriteLine("\nSelect filtering criteria:");
        Console.WriteLine($"1. Max Price (Current: {_criteria.MaxPrice?.ToString("C") ?? "Not Set"})");
        Console.WriteLine($"2. Departure Country (Current: {_criteria.DepartureCountry ?? "Not Set"})");
        Console.WriteLine($"3. Departure Airport (Current: {_criteria.DepartureAirport ?? "Not Set"})");
        Console.WriteLine($"4. Destination Country (Current: {_criteria.DestinationCountry ?? "Not Set"})");
        Console.WriteLine($"5. Destination Airport (Current: {_criteria.DestinationAirport ?? "Not Set"})");
        Console.WriteLine($"6. Departure Date (Current: {_criteria.DepartureDate?.ToString("yyyy-MM-dd") ?? "Not Set"})");
        Console.WriteLine("7. Confirm & Show Flights");
        Console.WriteLine("0. Reset filtering criteria");
        Console.Write("Enter your choice: ");
    }

    private bool HandleCriteriaMenuInput()
    {
        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {

            case "1":
                _criteria.MaxPrice = SearchInputHandlers.HandlePriceInput();
                break;

            case "2":
                _criteria.DepartureCountry = SearchInputHandlers.HandleDepartureCountryInput();
                break;

            case "3":
                _criteria.DepartureAirport = SearchInputHandlers.HandleDepartureAirportInput();
                break;

            case "4":
                _criteria.DestinationCountry = SearchInputHandlers.HandleDestinationCountryInput();
                break;

            case "5":
                _criteria.DestinationAirport = SearchInputHandlers.HandleDestinationAirportInput();
                break;

            case "6":
                _criteria.DepartureDate = SearchInputHandlers.HandleDepartureDateInput();
                break;

            case "7":
                return true;

            case "0":
                _criteria = new FlightSearchCriteria();
                Console.WriteLine("\nAll booking criteria have been reset.");
                break;

            default:
                Console.WriteLine("Invalid option. Please choose a number between 0 and 7.");
                break;
        }

        return false;
    }
}
