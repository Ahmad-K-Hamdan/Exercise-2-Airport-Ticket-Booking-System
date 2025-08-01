using Airport_Ticket_Booking_System.Enums;

namespace Airport_Ticket_Booking_System;

public class FlightValidator
{
    public static void ValidateFlightNumber()
    {

    }
    public static void ValidateCountry()
    {

    }
    public static void ValidateAirport()
    {

    }
    public static void ValidateDepartureDateTime()
    {

    }
    public static void ValidateFlightDuration()
    {

    }
    public static void ValidateCapacity()
    {

    }
    public static void ValidateBookedSeats()
    {

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
