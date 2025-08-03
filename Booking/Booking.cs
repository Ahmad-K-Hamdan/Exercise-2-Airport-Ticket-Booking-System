using Airport_Ticket_Booking_System.Enums;

namespace Airport_Ticket_Booking_System.Booking;

public class Booking
{
    public Guid BookingId { get; }
    public Flight Flight { get; }
    public FlightClass Class { get; }
    public DateTime BookingDate { get; }
    public int NumberOfSeats { get; }

    public Booking(Flight flight, FlightClass flightClass, int numberOfSeats)
    {
        BookingId = Guid.NewGuid();
        Flight = flight;
        Class = flightClass;
        BookingDate = DateTime.Now;
        NumberOfSeats = numberOfSeats;
    }
}
