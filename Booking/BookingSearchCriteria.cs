using Airport_Ticket_Booking_System.Enums;

namespace Airport_Ticket_Booking_System.Bookings;

public class BookingSearchCriteria
{
    public string? FlightNumber { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? DepartureCountry { get; set; }
    public string? DepartureAirport { get; set; }
    public string? DestinationCountry { get; set; }
    public string? DestinationAirport { get; set; }
    public DateTime? DepartureDate { get; set; }
    public FlightClass? FlightClass { get; set; }
}
