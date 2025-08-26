using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Airport_Ticket_Booking_System.Constants;
using Airport_Ticket_Booking_System.Enums;

namespace Airport_Ticket_Booking_System.Flights;

public class FlightValidator
{

    public static List<string> Validate(
        string flightNumber,
        string departureCountry,
        string departureAirport,
        DateTime departureDateTime,
        string destinationCountry,
        string destinationAirport,
        TimeSpan flightDuration,
        Dictionary<FlightClass, decimal> pricePerClass,
        int capacity,
        int bookedSeats
    )
    {
        var errors = new List<string>();

        try { ValidateFlightNumber(flightNumber); } catch (Exception ex) { errors.Add(ex.Message); }
        try { ValidateCountry(departureCountry); } catch (Exception ex) { errors.Add(ex.Message); }
        try { ValidateAirport(departureAirport); } catch (Exception ex) { errors.Add(ex.Message); }
        try { ValidateDepartureDateTime(departureDateTime); } catch (Exception ex) { errors.Add(ex.Message); }
        try { ValidateCountry(destinationCountry); } catch (Exception ex) { errors.Add(ex.Message); }
        try { ValidateAirport(destinationAirport); } catch (Exception ex) { errors.Add(ex.Message); }
        try { ValidateFlightDuration(flightDuration); } catch (Exception ex) { errors.Add(ex.Message); }
        try { ValidateCapacity(capacity); } catch (Exception ex) { errors.Add(ex.Message); }
        try { ValidateBookedSeats(bookedSeats, capacity); } catch (Exception ex) { errors.Add(ex.Message); }
        try { ValidatePricePerClass(pricePerClass); } catch (Exception ex) { errors.Add(ex.Message); }

        return errors;
    }

    public static string ValidateFlightNumber(string flightNumber)
    {
        if (string.IsNullOrWhiteSpace(flightNumber))
            throw new ArgumentException(ValidationMessages.FlightNumberEmpty);
        if (flightNumber.Length < 3 || flightNumber.Length > 10)
            throw new ArgumentException(ValidationMessages.FlightNumberLength);

        return flightNumber;
    }

    public static string ValidateCountry(string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException(ValidationMessages.CountryEmpty);
        if (country.Length < 2)
            throw new ArgumentException(ValidationMessages.CountryTooShort);
        if (!Regex.IsMatch(country, @"^[a-zA-Z]+$"))
            throw new ArgumentException(ValidationMessages.CountryLettersOnly);

        return country;
    }

    public static string ValidateAirport(string airport)
    {
        if (string.IsNullOrWhiteSpace(airport))
            throw new ArgumentException(ValidationMessages.AirportEmpty);
        if (airport.Length < 2)
            throw new ArgumentException(ValidationMessages.AirportTooShort);

        return airport;
    }

    public static DateTime ValidateDepartureDateTime(DateTime departure)
    {
        if (departure == default)
            throw new ArgumentException(ValidationMessages.DepartureNotSet);

        return departure;
    }

    public static TimeSpan ValidateFlightDuration(TimeSpan duration)
    {
        if (duration.TotalMinutes <= 0)
            throw new ArgumentException(ValidationMessages.DurationPositive);

        return duration;
    }

    public static int ValidateCapacity(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException(ValidationMessages.CapacityPositive);

        return capacity;
    }

    public static int ValidateBookedSeats(int bookedSeats, int capacity)
    {
        if (bookedSeats < 0)
            throw new ArgumentException(ValidationMessages.BookedSeatsNegative);
        if (bookedSeats > capacity)
            throw new ArgumentException(ValidationMessages.BookedSeatsExceed);

        return bookedSeats;
    }

    public static Dictionary<FlightClass, decimal> ValidatePricePerClass(
        Dictionary<FlightClass, decimal> pricePerClass
    )
    {
        foreach (FlightClass flightClass in Enum.GetValues<FlightClass>())
        {
            if (!pricePerClass.ContainsKey(flightClass))
                throw new ArgumentException(string.Format(ValidationMessages.MissingPrice, flightClass));
            if (pricePerClass[flightClass] <= 0)
                throw new ArgumentException(string.Format(ValidationMessages.NonPositivePrice, flightClass));
        }

        if (pricePerClass[FlightClass.Economy] != pricePerClass.Values.Min())
            throw new ArgumentException(ValidationMessages.EconomyLowest);
        else if (pricePerClass[FlightClass.FirstClass] != pricePerClass.Values.Max())
            throw new ArgumentException(ValidationMessages.FirstClassHighest);

        return pricePerClass;
    }

    public static List<string> GetValidationErrors(
        string flightNumber,
        string departureCountry,
        string departureAirport,
        DateTime departureDateTime,
        string destinationCountry,
        string destinationAirport,
        TimeSpan flightDuration,
        Dictionary<FlightClass, decimal> pricePerClass,
        int capacity,
        int bookedSeats
    )
    {
        var errors = new List<string>();

        try
        {
            ValidateFlightNumber(flightNumber);
        }
        catch (Exception ex)
        {
            errors.Add($"FlightNumber: {ex.Message}");
        }

        try
        {
            ValidateCountry(departureCountry);
        }
        catch (Exception ex)
        {
            errors.Add($"DepartureCountry: {ex.Message}");
        }

        try
        {
            ValidateAirport(departureAirport);
        }
        catch (Exception ex)
        {
            errors.Add($"DepartureAirport: {ex.Message}");
        }

        try
        {
            ValidateDepartureDateTime(departureDateTime);
        }
        catch (Exception ex)
        {
            errors.Add($"DepartureDateTime: {ex.Message}");
        }

        try
        {
            ValidateCountry(destinationCountry);
        }
        catch (Exception ex)
        {
            errors.Add($"DestinationCountry: {ex.Message}");
        }

        try
        {
            ValidateAirport(destinationAirport);
        }
        catch (Exception ex)
        {
            errors.Add($"DestinationAirport: {ex.Message}");
        }

        try
        {
            ValidateFlightDuration(flightDuration);
        }
        catch (Exception ex)
        {
            errors.Add($"FlightDuration: {ex.Message}");
        }

        try
        {
            ValidateCapacity(capacity);
        }
        catch (Exception ex)
        {
            errors.Add($"Capacity: {ex.Message}");
        }

        try
        {
            ValidateBookedSeats(bookedSeats, capacity);
        }
        catch (Exception ex)
        {
            errors.Add($"BookedSeats: {ex.Message}");
        }

        try
        {
            ValidatePricePerClass(pricePerClass);
        }
        catch (Exception ex)
        {
            errors.Add($"PricePerClass: {ex.Message}");
        }

        return errors;
    }

    public static void PrintModelValidationDetails<Flight>()
    {
        Console.WriteLine($"\n{typeof(Flight).Name} Model Validation Details:\n");

        var properties = typeof(Flight).GetProperties();

        foreach (var property in properties)
        {
            Console.WriteLine($"- {property.Name}:");
            Console.WriteLine($"    Type: {property.PropertyType.Name}");

            var attributes = property.GetCustomAttributes(true);
            var hasConstraints = false;

            foreach (var attribute in attributes)
            {
                if (attribute is RequiredAttribute)
                {
                    Console.WriteLine("    Constraint: Required");
                    hasConstraints = true;
                }
                else if (attribute is RangeAttribute range)
                {
                    Console.WriteLine($"    Constraint: Allowed Range({range.Minimum} -> {range.Maximum})");
                    hasConstraints = true;
                }
                else if (attribute is StringLengthAttribute strLen)
                {
                    Console.WriteLine($"    Constraint: String Length (Min: {strLen.MinimumLength} -> Max: {strLen.MaximumLength})");
                    hasConstraints = true;
                }
                else if (attribute is RegularExpressionAttribute regex)
                {
                    Console.WriteLine($"    Constraint: Must match pattern '{regex.Pattern}'");
                    hasConstraints = true;
                }
            }

            if (!hasConstraints)
                Console.WriteLine("    Constraint: None");

            Console.WriteLine();
        }
    }
}
