using Airport_Ticket_Booking_System.Enums;

namespace Airport_Ticket_Booking_System;

public static class FlightExtensions
{
    public static DateTime ArrivalDateTime(this Flight flight)
    {
        return flight.DepartureDateTime + flight.FlightDuration;
    }

    public static int AvailableSeats(this Flight flight)
    {
        return flight.Capacity - flight.BookedSeats;
    }

    public static Flight? FindFlightByNumber(this List<Flight> flights, string flightNumber)
    {
        return flights.FirstOrDefault(flight => flight.FlightNumber == flightNumber);
    }

    public static void ReserveSeats(this Flight flight, int NumberOfSeats)
    {
        flight.BookedSeats += NumberOfSeats;
    }

    public static string ToDetailedString(this Flight flight)
    {
        return $"Flight {flight.FlightNumber} | "
            + $"{flight.DepartureAirport} ({flight.DepartureCountry}) -> {flight.DestinationAirport} ({flight.DestinationCountry}) | "
            + $"Departs: {flight.DepartureDateTime:HH:mm yyyy-MM-dd} | "
            + $"Arrives: {flight.ArrivalDateTime():HH:mm yyyy-MM-dd} | "
            + $"Duration: {(int)flight.FlightDuration.TotalHours}h {flight.FlightDuration.Minutes}m | "
            + $"Seats: {flight.AvailableSeats()}/{flight.Capacity} available | "
            + $"Prices: Economy ${flight.PricePerClass[FlightClass.Economy]}, "
            + $"Business ${flight.PricePerClass[FlightClass.Business]}, "
            + $"First ${flight.PricePerClass[FlightClass.FirstClass]}";
    }

    public static void PrintAllFlights(this List<Flight> flights)
    {
        foreach (var flight in flights)
        {
            Console.WriteLine(flight.ToDetailedString());
        }
    }
}
