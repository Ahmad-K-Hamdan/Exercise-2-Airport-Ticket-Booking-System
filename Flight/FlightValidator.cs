using Airport_Ticket_Booking_System.Enums;

namespace Airport_Ticket_Booking_System;

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

    public static Dictionary<FlightClass, decimal> ValidatePricePerClass(Dictionary<FlightClass, decimal> pricePerClass)
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
}
