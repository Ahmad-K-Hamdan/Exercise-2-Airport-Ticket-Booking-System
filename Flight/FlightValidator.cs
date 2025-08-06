using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Airport_Ticket_Booking_System.Enums;

namespace Airport_Ticket_Booking_System.Flights;

public class FlightValidator
{
    public static string ValidateFlightNumber(string flightNumber)
    {
        if (string.IsNullOrWhiteSpace(flightNumber))
            throw new ArgumentException("Flight number cannot be empty.");
        if (flightNumber.Length < 3 || flightNumber.Length > 6)
            throw new ArgumentException("Flight number must be between 3 and 10 characters.");

        return flightNumber;
    }

    public static string ValidateCountry(string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be empty.");
        if (country.Length < 2)
            throw new ArgumentException("Country name is too short.");
        if (!Regex.IsMatch(country, @"^[a-zA-Z]+$"))
            throw new ArgumentException("Country name must contain only letters.");

        return country;
    }

    public static string ValidateAirport(string airport)
    {
        if (string.IsNullOrWhiteSpace(airport))
            throw new ArgumentException("Airport cannot be empty.");
        if (airport.Length < 2)
            throw new ArgumentException("Airport name is too short.");

        return airport;
    }

    public static DateTime ValidateDepartureDateTime(DateTime departure)
    {
        if (departure == default)
            throw new ArgumentException("Departure time is not set.");

        return departure;
    }

    public static TimeSpan ValidateFlightDuration(TimeSpan duration)
    {
        if (duration.TotalMinutes <= 0)
            throw new ArgumentException("Flight duration must be positive.");

        return duration;
    }

    public static int ValidateCapacity(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("Flight capacity must be greater than zero.");

        return capacity;
    }

    public static int ValidateBookedSeats(int bookedSeats, int capacity)
    {
        if (bookedSeats < 0)
            throw new ArgumentException("Booked seats cannot be negative.");
        if (bookedSeats > capacity)
            throw new ArgumentException("Booked seats cannot exceed flight capacity.");

        return bookedSeats;
    }

    public static Dictionary<FlightClass, decimal> ValidatePricePerClass(
        Dictionary<FlightClass, decimal> pricePerClass
    )
    {
        foreach (FlightClass flightClass in Enum.GetValues<FlightClass>())
        {
            if (!pricePerClass.ContainsKey(flightClass))
                throw new ArgumentException($"Missing price for {flightClass}");
            if (pricePerClass[flightClass] <= 0)
                throw new ArgumentException($"Price for {flightClass} cannot be non-positive");
        }

        if (pricePerClass[FlightClass.Economy] != pricePerClass.Values.Min())
            throw new ArgumentException("Price for economy must be the lowest");
        else if (pricePerClass[FlightClass.FirstClass] != pricePerClass.Values.Max())
            throw new ArgumentException("Price for first class must be the highest");

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
}
