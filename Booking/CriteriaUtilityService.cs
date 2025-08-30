using AirportTicketBookingSystem.Handlers;

namespace Airport_Ticket_Booking_System.Bookings;

public class CriteriaUtilityService
{
    private BookingSearchQuery _criteria;

    public CriteriaUtilityService(BookingSearchQuery criteria)
    {
        _criteria = criteria;
    }

    public void ShowCriteriaMenu()
    {
        Console.WriteLine("\nSelect filtering criteria:");
        Console.WriteLine($"1. Flight Number (Current: {_criteria.FlightNumber ?? "Not Set"})");
        Console.WriteLine($"2. Max Price (Current: {_criteria.MaxPrice?.ToString("C") ?? "Not Set"})");
        Console.WriteLine($"3. Departure Country (Current: {_criteria.DepartureCountry ?? "Not Set"})");
        Console.WriteLine($"4. Departure Airport (Current: {_criteria.DepartureAirport ?? "Not Set"})");
        Console.WriteLine($"5. Destination Country (Current: {_criteria.DestinationCountry ?? "Not Set"})");
        Console.WriteLine($"6. Destination Airport (Current: {_criteria.DestinationAirport ?? "Not Set"})");
        Console.WriteLine($"7. Departure Date (Current: {_criteria.DepartureDate?.ToString("yyyy-MM-dd") ?? "Not Set"})");
        Console.WriteLine($"8. Flight Class (Current: {_criteria.FlightClass?.ToString() ?? "Not Set"})");
        Console.WriteLine("9. Confirm & Show Bookings");
        Console.WriteLine("0. Reset filtering criteria");
        Console.Write("Enter your choice: ");
    }

    public bool HandleCriteriaMenuInput()
    {
        string choice = Console.ReadLine()?.ToLower() ?? "";

        switch (choice)
        {
            case "1":
                _criteria.FlightNumber = SearchInputHandlers.HandleFlightNumberInput();
                break;

            case "2":
                _criteria.MaxPrice = SearchInputHandlers.HandlePriceInput();
                break;

            case "3":
                _criteria.DepartureCountry = SearchInputHandlers.HandleDepartureCountryInput();
                break;

            case "4":
                _criteria.DepartureAirport = SearchInputHandlers.HandleDepartureAirportInput();
                break;

            case "5":
                _criteria.DestinationCountry = SearchInputHandlers.HandleDestinationCountryInput();
                break;

            case "6":
                _criteria.DestinationAirport = SearchInputHandlers.HandleDestinationAirportInput();
                break;

            case "7":
                _criteria.DepartureDate = SearchInputHandlers.HandleDepartureDateInput();
                break;

            case "8":
                _criteria.FlightClass = SearchInputHandlers.HandleFlightClassInput();
                break;

            case "9":
                return true;

            case "0":
                _criteria = new BookingSearchQuery();
                Console.WriteLine("\nAll booking criteria have been reset.");
                break;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }

        return false;
    }
}