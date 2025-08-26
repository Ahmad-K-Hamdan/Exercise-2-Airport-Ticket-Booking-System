namespace Airport_Ticket_Booking_System.Flights;

public class FlightService
{
    public Flight? FindFlightByNumber(string flightNumber)
    {
        var flights = FlightsRepository.GetFlights();
        return flights.FirstOrDefault(f => f.FlightNumber == flightNumber);
    }

    public List<Flight> GetAllFlights()
    {
        return FlightsRepository.GetFlights();
    }

    public void PrintAllFlights()
    {
        var flights = FlightsRepository.GetFlights();

        if (flights.Count == 0)
        {
            Console.WriteLine("\nThere are no flights available.");
            return;
        }

        Console.WriteLine("\nAll available flights:\n");
        foreach (var flight in flights)
        {
            Console.WriteLine(flight.ToDetailedString());
        }
    }
}
