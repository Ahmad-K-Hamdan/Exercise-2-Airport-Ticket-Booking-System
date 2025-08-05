using System.Data.Common;
using Airport_Ticket_Booking_System.Enums;

namespace Airport_Ticket_Booking_System.Booking;

public static class BookingExtensions
{
    public static decimal CalculatePricePaid(this Booking booking)
    {
        if (booking.Flight.PricePerClass.TryGetValue(booking.Class, out var pricePerSeat))
        {
            return pricePerSeat * booking.NumberOfSeats;
        }
        throw new ArgumentException($"Price not found for class {booking.Class}");
    }

    public static void BookFlight(this List<Booking> bookings, Flight flight, FlightClass flightClass, int seatCount)
    {
        var booking = new Booking(flight, flightClass, seatCount);
        bookings.Add(booking);
        Console.WriteLine($"Successfully booked for {seatCount} on flight {flight.FlightNumber}.");
    }

    public static void CancelBooking(this List<Booking> bookings, Guid bookingId)
    {
        var booking = FindBookingById(bookings, bookingId);
        if (booking is null)
        {
            Console.WriteLine($"No booking with ID {bookingId}");
            return;
        }
        bookings.Remove(booking!);
    }

    public static void ModifyBooking(this List<Booking> bookings, List<Flight> flights, Guid bookingId)
    {
        var booking = FindBookingById(bookings, bookingId);
        if (booking is null)
        {
            Console.WriteLine($"No booking with ID {bookingId}");
            return;
        }
        changeBookingInformation(booking, flights);
        Console.WriteLine($"Booking {bookingId} modified successfully.");
    }

    private static void changeBookingInformation(Booking booking, List<Flight> flights)
    {
        Console.WriteLine("\n--- Current Booking Details ---");
        Console.WriteLine(booking.ToDetailedString());

        Console.WriteLine("\n--- Modify Booking ---");

        Console.Write("Change flight? (y/n): ");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            Console.Write("Enter new flight number: ");
            string newFlightNumber = Console.ReadLine()!;

            var newFlight = flights.FindFlightByNumber(newFlightNumber!);
            if (newFlight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            booking.Flight = newFlight;
        }

        Console.Write("Change class? (y/n): ");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            Console.Write("Enter new class (Economy, Business, First Class): ");
            if (Enum.TryParse<FlightClass>(Console.ReadLine(), true, out var newClass))
            {
                booking.Class = newClass;
            }
            else
            {
                Console.WriteLine("Invalid class.");
                return;
            }
        }

        Console.Write("Change number of seats? (y/n): ");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            Console.Write("Enter new number of seats: ");
            if (int.TryParse(Console.ReadLine(), out var newSeats) && newSeats > 0)
            {
                booking.NumberOfSeats = newSeats;
            }
            else
            {
                Console.WriteLine("Invalid number.");
                return;
            }
        }

        booking.BookingDate = DateTime.Now;
    }

    public static void ViewBookings(this List<Booking> bookings)
    {
        foreach (var booking in bookings)
        {
            Console.WriteLine(booking.ToDetailedString());
        }
    }

    private static Booking? FindBookingById(this List<Booking> bookings, Guid bookingId)
    {
        return bookings.SingleOrDefault(booking => booking.BookingId == bookingId);
    }

    public static string ToDetailedString(this Booking booking)
    {
        return $"\n****************************************************\n" +
                $"Booking ID   : {booking.BookingId}\n" +
                $"Flight No    : {booking.Flight.FlightNumber}\n" +
                $"From         : {booking.Flight.DepartureAirport}, {booking.Flight.DepartureCountry}\n" +
                $"To           : {booking.Flight.DestinationAirport}, {booking.Flight.DestinationCountry}\n" +
                $"Departure    : {booking.Flight.DepartureDateTime}\n" +
                $"Class        : {booking.Class}\n" +
                $"Seats        : {booking.NumberOfSeats}\n" +
                $"Price Paid   : {booking.CalculatePricePaid():C}\n" +
                $"Booked At    : {booking.BookingDate}";
    }
}
