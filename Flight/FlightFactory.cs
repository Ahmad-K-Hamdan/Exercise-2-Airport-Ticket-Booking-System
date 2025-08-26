using Airport_Ticket_Booking_System.Enums;

namespace Airport_Ticket_Booking_System.Flights;

public static class FlightFactory
{
    public static Flight CreateFlight(
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
        var errors = FlightValidator.GetValidationErrors(
            flightNumber,
            departureCountry,
            departureAirport,
            departureDateTime,
            destinationCountry,
            destinationAirport,
            flightDuration,
            pricePerClass,
            capacity,
            bookedSeats
        );

        if (errors.Any())
        {
            throw new ArgumentException("Flight validation failed: \n" + string.Join("\n", errors));
        }

        return new Flight(
            flightNumber,
            departureCountry,
            departureAirport,
            departureDateTime,
            destinationCountry,
            destinationAirport,
            flightDuration,
            pricePerClass,
            capacity,
            bookedSeats
        );
    }
}
