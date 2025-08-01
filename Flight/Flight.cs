using Airport_Ticket_Booking_System.Enums;

namespace Airport_Ticket_Booking_System;

public class Flight
{
    public string FlightNumber { get; }
    public string DepartureCountry { get; }
    public string DepartureAirport { get; }
    public DateTime DepartureDateTime { get; }
    public string DestinationCountry { get; }
    public string DestinationAirport { get; }
    public TimeSpan FlightDuration { get; }
    public Dictionary<FlightClass, decimal> PricePerClass { get; }
    public int Capacity { get; }
    public int BookedSeats { get; }

    public Flight(string flightNumber, string departureCountry, string departureAirport, DateTime departureDateTime, string destinationCountry, string destinationAirport, TimeSpan flightDuration, Dictionary<FlightClass, decimal> pricePerClass, int capacity, int bookedSeats)
    {
        FlightNumber = flightNumber;
        DepartureCountry = departureCountry;
        DepartureAirport = departureAirport;
        DepartureDateTime = departureDateTime;
        DestinationCountry = destinationCountry;
        DestinationAirport = destinationAirport;
        FlightDuration = flightDuration;
        Capacity = capacity;
        BookedSeats = bookedSeats;
        PricePerClass = FlightValidator.ValidatePricePerClass(pricePerClass);
    }
}
