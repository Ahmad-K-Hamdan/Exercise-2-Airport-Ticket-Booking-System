using Airport_Ticket_Booking_System.Enums;
using Airport_Ticket_Booking_System.Flights;
using System.Globalization;

namespace Airport_Ticket_Booking_System.Bookings;

public static class BookingsRepository
{
    public static List<Booking> GetBookings(List<Flight> flights, string filePath = "Data/Bookings.csv")
    {
        var bookings = new List<Booking>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return bookings;
        }

        try
        {
            var lines = File.ReadAllLines(filePath).Skip(1);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var columns = line.Split(',');
                try
                {
                    var bookingId = int.Parse(columns[0]);
                    var flightNumber = columns[1];
                    var passengerId = int.Parse(columns[2]);

                    if (!Enum.TryParse<FlightClass>(columns[3], true, out var flightClass))
                    {
                        Console.WriteLine($"Invalid flight class '{columns[3]}' for booking {bookingId}.");
                        continue;
                    }

                    var bookingDate = DateTime.Parse(columns[4], CultureInfo.InvariantCulture);
                    var numberOfSeats = int.Parse(columns[5]);

                    var flight = flights.FirstOrDefault(f => f.FlightNumber == flightNumber);
                    if (flight == null)
                    {
                        Console.WriteLine($"Flight {flightNumber} not found for booking {bookingId}.");
                        continue;
                    }

                    var booking = new Booking(bookingId, flight, passengerId, flightClass, bookingDate, numberOfSeats);
                    bookings.Add(booking);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to parse line: {line}\nError: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }

        return bookings;
    }

    public static void SaveBookings(List<Booking> bookings, string filePath = "Data/Bookings.csv")
    {
        if (bookings == null || bookings.Count == 0)
        {
            Console.WriteLine("No bookings to save.");
            return;
        }

        var lines = new List<string> { "BookingId,FlightNumber,PassengerId,Class,BookingDate,NumberOfSeats" };

        lines.AddRange(bookings.Select(b =>
            $"{b.BookingId},{b.Flight.FlightNumber},{b.PassengerId},{b.Class},{b.BookingDate},{b.NumberOfSeats}"
        ));

        try
        {
            File.WriteAllLines(filePath, lines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save bookings: {ex.Message}");
        }
    }
}
