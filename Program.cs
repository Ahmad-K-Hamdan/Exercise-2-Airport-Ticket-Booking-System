using Airport_Ticket_Booking_System;
using Airport_Ticket_Booking_System.Booking;
using Airport_Ticket_Booking_System.Enums;

class Program
{
    static void Main()
    {
        Console.WriteLine();
        var AllFlights = FlightImporter.ImportFlights();
        Console.WriteLine();
        AllFlights.PrintAllFlights();

        var bookings = new List<Booking>();
        var myFlight1 = AllFlights.FirstOrDefault();
        var myFlight2 = AllFlights.Skip(1).FirstOrDefault();

        bookings.BookFlight(myFlight1!, FlightClass.Economy, 3);
        bookings.BookFlight(myFlight2!, FlightClass.FirstClass, 2);
        
        Guid guid = bookings.FirstOrDefault()!.BookingId;
        bookings.ModifyBooking(AllFlights, guid);

        bookings.ViewBookings();
    }

}
