using AirportTicketBookingSystem.Handlers;

namespace Airport_Ticket_Booking_System.Flights;

public class CriteriaUtilityService
{
    private FlightSearchQuery _criteria;

    public CriteriaUtilityService(FlightSearchQuery criteria)
    {
        _criteria = criteria;
    }

    public void ShowCriteriaMenu()
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

    public bool HandleCriteriaMenuInput()
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
                _criteria = new FlightSearchQuery();
                Console.WriteLine("\nAll booking criteria have been reset.");
                break;

            default:
                Console.WriteLine("Invalid option. Please choose a number between 0 and 7.");
                break;
        }

        return false;
    }
}