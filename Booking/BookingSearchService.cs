using Airport_Ticket_Booking_System.Enums;
using AirportTicketBookingSystem.Handlers;

namespace Airport_Ticket_Booking_System.Bookings;

public class BookingSearchService
{
    private readonly object[] UserCriteria = new object[8];

    public void ShowCriteriaMenu()
    {
        Console.WriteLine("\nSelect filtering criteria:");
        Console.WriteLine($"1. Flight Number (Current: {Display(UserCriteria[0])})");
        Console.WriteLine($"2. Max Price (Current: {Display(UserCriteria[1])})");
        Console.WriteLine($"3. Departure Country (Current: {Display(UserCriteria[2])})");
        Console.WriteLine($"4. Departure Airport (Current: {Display(UserCriteria[3])})");
        Console.WriteLine($"5. Destination Country (Current: {Display(UserCriteria[4])})");
        Console.WriteLine($"6. Destination Airport (Current: {Display(UserCriteria[5])})");
        Console.WriteLine($"7. Departure Date (Current: {Display(UserCriteria[6])})");
        Console.WriteLine($"8. Flight Class (Current: {Display(UserCriteria[7])})");
        Console.WriteLine("9. Confirm & Show Bookings");
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
        string choice = Console.ReadLine()?.ToLower() ?? "";

        switch (choice)
        {
            case "1":
                UserCriteria[0] = SearchInputHandlers.HandleFlightNumberInput();
                break;

            case "2":
                UserCriteria[1] = SearchInputHandlers.HandlePriceInput();
                break;

            case "3":
                UserCriteria[2] = SearchInputHandlers.HandleDepartureCountryInput();
                break;

            case "4":
                UserCriteria[3] = SearchInputHandlers.HandleDepartureAirportInput();
                break;

            case "5":
                UserCriteria[4] = SearchInputHandlers.HandleDestinationCountryInput();
                break;

            case "6":
                UserCriteria[5] = SearchInputHandlers.HandleDestinationAirportInput();
                break;

            case "7":
                UserCriteria[6] = SearchInputHandlers.HandleDepartureDateInput();
                break;

            case "8":
                UserCriteria[7] = SearchInputHandlers.HandleFlightClassInput();
                break;

            case "9":
                return true;

            case "0":
                ResetCriteria();
                break;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }

        return false;
    }

    public List<Booking> FilterBookings(List<Booking> bookings)
    {
        IEnumerable<Booking> query = bookings;

        if (UserCriteria[0] is string flightNumber)
        {
            query = query.Where(booking =>
            booking.Flight.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));
        }

        if (UserCriteria[1] is decimal maxPrice)
        {
            query = query.Where(booking =>
            booking.Flight.PricePerClass.Values.Any(price => price <= maxPrice)
            );
        }

        if (UserCriteria[2] is string depCountry)
        {
            query = query.Where(booking =>
            booking.Flight.DepartureCountry.Equals(depCountry, StringComparison.OrdinalIgnoreCase)
            );
        }

        if (UserCriteria[3] is string depAirport)
        {
            query = query.Where(booking =>
            booking.Flight.DepartureAirport.Equals(depAirport, StringComparison.OrdinalIgnoreCase)
            );
        }

        if (UserCriteria[4] is string destCountry)
        {
            query = query.Where(booking =>
            booking.Flight.DestinationCountry.Equals(destCountry, StringComparison.OrdinalIgnoreCase)
            );
        }

        if (UserCriteria[5] is string destAirport)
        {
            query = query.Where(booking =>
            booking.Flight.DestinationAirport.Equals(destAirport, StringComparison.OrdinalIgnoreCase)
            );
        }

        if (UserCriteria[6] is DateTime depDate)
        {
            query = query.Where(booking =>
            booking.Flight.DepartureDateTime.Date == depDate.Date);
        }

        if (UserCriteria[7] is FlightClass flightClass)
        {
            query = query.Where(booking =>
            booking.Class == flightClass);
        }

        return query.ToList();
    }

    private void ResetCriteria()
    {
        for (int i = 0; i < UserCriteria.Length; i++)
            UserCriteria[i] = null!;
        Console.WriteLine("\nAll booking criteria have been reset.");
    }
}
