namespace Airport_Ticket_Booking_System.Flights;

public class FlightSearchQuery
{
    public decimal? MaxPrice { get; set; }
    public string? DepartureCountry { get; set; }
    public string? DepartureAirport { get; set; }
    public string? DestinationCountry { get; set; }
    public string? DestinationAirport { get; set; }
    public DateTime? DepartureDate { get; set; }
}