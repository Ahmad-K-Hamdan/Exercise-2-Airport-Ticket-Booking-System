using Airport_Ticket_Booking_System.Enums;
using System.ComponentModel.DataAnnotations;

namespace Airport_Ticket_Booking_System.Flights;

public class Flight
{
    [Required(ErrorMessage = "Flight number cannot be empty.")]
    [StringLength(6, MinimumLength = 3, ErrorMessage = "Flight number must be between 3 and 6 characters")]
    public string FlightNumber { get; }

    [Required(ErrorMessage = "Flight departure country cannot be empty.")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Country name must contain only letters")]
    [MinLength(2)]
    public string DepartureCountry { get; }

    [Required(ErrorMessage = "Flight departure airport cannot be empty.")]
    [MinLength(2)]
    public string DepartureAirport { get; }

    [Required(ErrorMessage = "Flight departure time cannot be empty.")]
    [DataType(DataType.DateTime)]
    public DateTime DepartureDateTime { get; }

    [Required(ErrorMessage = "Flight destination country cannot be empty.")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Country name must contain only letters")]
    [MinLength(2)]
    public string DestinationCountry { get; }

    [Required(ErrorMessage = "Flight destination airport cannot be empty.")]
    [MinLength(2)]
    public string DestinationAirport { get; }

    [Required(ErrorMessage = "Flight duration cannot be empty.")]
    [Range(1, int.MaxValue, ErrorMessage = "Flight duration must be positive")]
    public TimeSpan FlightDuration { get; }

    [Required(ErrorMessage = "Flight class cannot be empty.")]
    public Dictionary<FlightClass, decimal> PricePerClass { get; }

    [Required(ErrorMessage = "Flight capacity cannot be empty.")]
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero")]
    public int Capacity { get; }

    [Required(ErrorMessage = "Flight booked seats cannot be empty.")]
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
        FlightNumber = flightNumber;
        DepartureCountry = departureCountry;
        DepartureAirport = departureAirport;
        DepartureDateTime = departureDateTime;
        DestinationCountry = destinationCountry;
        DestinationAirport = destinationAirport;
        FlightDuration = flightDuration;
        Capacity = capacity;
        BookedSeats = bookedSeats;
        PricePerClass = pricePerClass;
    }
}
