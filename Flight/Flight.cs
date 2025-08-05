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
    public int BookedSeats { get; set; }

    public Flight(
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
        FlightNumber = FlightValidator.ValidateFlightNumber(flightNumber);
        DepartureCountry = FlightValidator.ValidateCountry(departureCountry);
        DepartureAirport = FlightValidator.ValidateAirport(departureAirport);
        DepartureDateTime = FlightValidator.ValidateDepartureDateTime(departureDateTime);
        DestinationCountry = FlightValidator.ValidateCountry(destinationCountry);
        DestinationAirport = FlightValidator.ValidateAirport(destinationAirport);
        FlightDuration = FlightValidator.ValidateFlightDuration(flightDuration);
        Capacity = FlightValidator.ValidateCapacity(capacity);
        BookedSeats = FlightValidator.ValidateBookedSeats(bookedSeats, capacity);
        PricePerClass = FlightValidator.ValidatePricePerClass(pricePerClass);
    }
}
