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
}
