using AirportTicketBookingSystem.Handlers;

namespace Airport_Ticket_Booking_System.Flights;

public class FlightSearchService
{
    private readonly object[] UserCriteria = new object[6];

    public void ShowCriteriaMenu()
    {
        Console.WriteLine("\nSelect filtering criteria:");
        Console.WriteLine($"1. Price (Current: {Display(UserCriteria[0])})");
        Console.WriteLine($"2. Departure Country (Current: {Display(UserCriteria[1])})");
        Console.WriteLine($"3. Departure Airport (Current: {Display(UserCriteria[2])})");
        Console.WriteLine($"4. Destination Country (Current: {Display(UserCriteria[3])})");
        Console.WriteLine($"5. Destination Airport (Current: {Display(UserCriteria[4])})");
        Console.WriteLine($"6. Departure Date (Current: {Display(UserCriteria[5])})");
        Console.WriteLine("7. Confirm & Show Flights");
        Console.WriteLine("0. Reset filtering criteria");
        Console.Write("Enter your choice: ");
    }

    private string Display(object value)
    {
        if (value == null)
            return "Not Set";

        else if (value is DateTime dateTime)
            return dateTime.ToString("yyyy-MM-dd");

        else if (value is decimal maxPrice)
            return $"≤ {maxPrice:C}";

        return value.ToString()!;
    }

    public bool HandleCriteriaMenuInput()
    {
        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
            case "1":
                UserCriteria[0] = SearchInputHandlers.HandlePriceInput();
                break;

            case "2":
                UserCriteria[1] = SearchInputHandlers.HandleDepartureCountryInput();
                break;

            case "3":
                UserCriteria[2] = SearchInputHandlers.HandleDepartureAirportInput();
                break;

            case "4":
                UserCriteria[3] = SearchInputHandlers.HandleDestinationCountryInput();
                break;

            case "5":
                UserCriteria[4] = SearchInputHandlers.HandleDestinationAirportInput();
                break;

            case "6":
                UserCriteria[5] = SearchInputHandlers.HandleDepartureDateInput();
                break;

            case "7":
                return true;

            case "0":
                ResetCriteria();
                break;

            default:
                Console.WriteLine("Invalid option. Please choose a number between 0 and 8.");
                break;
        }

        return false;
    }

    public List<Flight> FilterFlights(List<Flight> flights)
    {
        IEnumerable<Flight> query = flights;

        if (UserCriteria[0] is decimal maxPrice)
        {
            query = query.Where(flight =>
                flight.PricePerClass.Values.Any(price => price <= maxPrice)
            );
        }

        if (UserCriteria[1] is string depCountry)
        {
            query = query.Where(flight =>
                flight.DepartureCountry.Equals(depCountry, StringComparison.OrdinalIgnoreCase)
            );
        }

        if (UserCriteria[2] is string depAirport)
        {
            query = query.Where(flight =>
                flight.DepartureAirport.Equals(depAirport, StringComparison.OrdinalIgnoreCase)
            );
        }

        if (UserCriteria[3] is string destCountry)
        {
            query = query.Where(flight =>
                flight.DestinationCountry.Equals(
                    destCountry,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }

        if (UserCriteria[4] is string destAirport)
        {
            query = query.Where(flight =>
                flight.DestinationAirport.Equals(
                    destAirport,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }

        if (UserCriteria[5] is DateTime depDate)
        {
            query = query.Where(flight => flight.DepartureDateTime.Date == depDate.Date);
        }

        return query.ToList();
    }

    private void ResetCriteria()
    {
        for (int i = 0; i < UserCriteria.Length; i++)
        {
            UserCriteria[i] = null!;
        }
        Console.WriteLine("\nAll search criteria have been reset.");
    }
}
