using Airport_Ticket_Booking_System.Enums;
using Airport_Ticket_Booking_System.Flights;

namespace Airport_Ticket_Booking_System.Bookings;

public class Booking
{
    public int BookingId { get; set; }
    public Flight Flight { get; set; }
    public FlightClass Class { get; set; }
    public DateTime BookingDate { get; set; }
    public int NumberOfSeats { get; set; }

    private readonly int NextBookingId = 1000;

    public Booking(Flight flight, FlightClass flightClass, int numberOfSeats)
    {
        BookingId = NextBookingId;
        Flight = flight;
        Class = flightClass;
        BookingDate = DateTime.Now;
        NumberOfSeats = numberOfSeats;
    }
}
