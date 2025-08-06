using Airport_Ticket_Booking_System.Flights;

namespace AirportTicketBookingSystem.Handlers;

public static class SearchInputHandlers
{
    public static object HandlePriceInput()
    {
        while (true)
        {
            Console.Write("Enter max price: ");
            string? priceInput = Console.ReadLine();
            if (decimal.TryParse(priceInput, out decimal price) && price >= 0)
                return price;

            Console.WriteLine("Invalid price. Please enter a valid positive decimal number.");
        }
    }

    public static object HandleDepartureCountryInput()
    {
        while (true)
        {
            Console.Write("Enter departure country: ");
            string? input = Console.ReadLine();
            try
            {
                return FlightValidator.ValidateCountry(input!);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nInvalid country: {ex.Message}");
            }
        }
    }

    public static object HandleDepartureAirportInput()
    {
        while (true)
        {
            Console.Write("Enter departure airport: ");
            string? input = Console.ReadLine();
            try
            {
                return FlightValidator.ValidateAirport(input!);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nInvalid airport: {ex.Message}");
            }
        }
    }

    public static object HandleDestinationCountryInput()
    {
        while (true)
        {
            Console.Write("Enter destination country: ");
            string? input = Console.ReadLine();
            try
            {
                return FlightValidator.ValidateCountry(input!);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nInvalid country: {ex.Message}");
            }
        }
    }

    public static object HandleDestinationAirportInput()
    {
        while (true)
        {
            Console.Write("Enter destination airport: ");
            string? input = Console.ReadLine();
            try
            {
                return FlightValidator.ValidateAirport(input!);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nInvalid airport: {ex.Message}");
            }
        }
    }

    public static object HandleDepartureDateInput()
    {
        while (true)
        {
            Console.Write("Enter departure date (yyyy-MM-dd): ");
            string? dateInput = Console.ReadLine();

            if (DateTime.TryParse(dateInput, out DateTime parsedDate))
                return parsedDate.Date;

            Console.WriteLine("Invalid date format.");
        }
    }

    public static object HandleFlightNumberInput()
    {
        while (true)
        {
            Console.Write("Enter flight number: ");
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.WriteLine("Flight number cannot be empty.");
        }
    }

    public static object HandlePassengerNameInput()
    {
        while (true)
        {
            Console.Write("Enter passenger name: ");
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.WriteLine("Passenger name cannot be empty.");
        }
    }

    public static object HandleFlightClassInput()
    {
        while (true)
        {
            Console.Write("Enter flight class (e.g., Economy, Business, FirstClass): ");
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.WriteLine("Flight class cannot be empty.");
        }
    }
}