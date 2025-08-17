namespace Airport_Ticket_Booking_System.Bookings;

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

    public static string ToDetailedString(this Booking booking)
    {
        return $"\n****************************************************\n"
            + $"Booking ID   : {booking.BookingId}\n"
            + $"Flight No    : {booking.Flight.FlightNumber}\n"
            + $"Passenger ID    : {booking.PassengerId}\n"
            + $"From         : {booking.Flight.DepartureAirport}, {booking.Flight.DepartureCountry}\n"
            + $"To           : {booking.Flight.DestinationAirport}, {booking.Flight.DestinationCountry}\n"
            + $"Departure    : {booking.Flight.DepartureDateTime}\n"
            + $"Class        : {booking.Class}\n"
            + $"Seats        : {booking.NumberOfSeats}\n"
            + $"Price Paid   : {booking.CalculatePricePaid():C}\n"
            + $"Booked At    : {booking.BookingDate}";
    }
}
