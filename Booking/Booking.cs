using Airport_Ticket_Booking_System.Enums;

namespace Airport_Ticket_Booking_System.Booking;

public class Booking
{
    public Guid BookingId { get; set; }
    public Flight Flight { get; set; }
    public FlightClass Class { get; set; }
    public DateTime BookingDate { get; set; }
    public int NumberOfSeats { get; set; }

    public Booking(Flight flight, FlightClass flightClass, int numberOfSeats)
    {
        BookingId = Guid.NewGuid();
        Flight = flight;
        Class = flightClass;
        BookingDate = DateTime.Now;
        NumberOfSeats = numberOfSeats;
    }
}
