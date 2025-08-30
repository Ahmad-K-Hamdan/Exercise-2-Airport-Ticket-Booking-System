using Airport_Ticket_Booking_System.Enums;
using Airport_Ticket_Booking_System.Flights;

namespace Airport_Ticket_Booking_System.Bookings;

public class Booking
{
    public int BookingId { get; set; }
    public Flight Flight { get; set; }
    public int PassengerId { get; set; }
    public FlightClass Class { get; set; }
    public DateTime BookingDate { get; set; }
    public int NumberOfSeats { get; set; }

    private static int _nextBookingId = 1000;

    public Booking(Flight flight, int passengerId, FlightClass flightClass, int numberOfSeats)
    {
        BookingId = _nextBookingId++;
        Flight = flight;
        Class = flightClass;
        PassengerId = passengerId;
        BookingDate = DateTime.Now;
        NumberOfSeats = numberOfSeats;
    }

    public Booking(int bookingId, Flight flight, int passengerId, FlightClass flightClass, DateTime bookingDate, int numberOfSeats)
    {
        BookingId = bookingId;
        Flight = flight;
        Class = flightClass;
        PassengerId = passengerId;
        BookingDate = bookingDate;
        NumberOfSeats = numberOfSeats;

        _nextBookingId = Math.Max(_nextBookingId, bookingId + 1);
    }
}
