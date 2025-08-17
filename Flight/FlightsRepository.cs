using Airport_Ticket_Booking_System.Enums;
using System.Globalization;

namespace Airport_Ticket_Booking_System.Flights;

public static class FlightsRepository
{
    public static List<Flight> GetFlights(string filePath = "Data/Flights.csv")
    {
        var flights = new List<Flight>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return flights;
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
                    var priceParts = columns[7].Split('/');
                    if (priceParts.Length != 3)
                    {
                        Console.WriteLine($"Invalid price format for flight {columns[0]}.");
                        continue;
                    }

                    var prices = new Dictionary<FlightClass, decimal>
                    {
                        { FlightClass.Economy, decimal.Parse(priceParts[0].Trim()) },
                        { FlightClass.Business, decimal.Parse(priceParts[1].Trim()) },
                        { FlightClass.FirstClass, decimal.Parse(priceParts[2].Trim()) }
                    };

                    var flightNumber = columns[0];
                    var departureCountry = columns[1];
                    var departureAirport = columns[2];
                    var departureDateTime = DateTime.Parse(columns[3], CultureInfo.InvariantCulture);
                    var destinationCountry = columns[4];
                    var destinationAirport = columns[5];
                    var flightDuration = TimeSpan.Parse(columns[6]);
                    var capacity = int.Parse(columns[8]);
                    var bookedSeats = int.Parse(columns[9]);

                    var errors = FlightValidator.GetValidationErrors(
                        flightNumber,
                        departureCountry,
                        departureAirport,
                        departureDateTime,
                        destinationCountry,
                        destinationAirport,
                        flightDuration,
                        prices,
                        capacity,
                        bookedSeats
                    );

                    if (errors.Count > 0)
                    {
                        Console.WriteLine($"Validation errors for flight {flightNumber}: {string.Join("; ", errors)}");
                        continue;
                    }

                    var flight = new Flight(
                        flightNumber,
                        departureCountry,
                        departureAirport,
                        departureDateTime,
                        destinationCountry,
                        destinationAirport,
                        flightDuration,
                        prices,
                        capacity,
                        bookedSeats
                    );

                    flights.Add(flight);
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

        return flights;
    }

    public static void SaveFlights(List<Flight> flights, string filePath = "Data/Flights.csv")
    {
        if (flights == null || flights.Count == 0)
        {
            Console.WriteLine("No flights to save.");
            return;
        }

        var lines = new List<string>
        {
            "Flight Number,Departure Country,Departure Airport,Departure DateTime,Destination Country,Destination Airport,Flight Duration,Price Per Class (Economy / Business / FirstClass),Capacity,Booked Seats"
        };

        lines.AddRange(flights.Select(f =>
            $"{f.FlightNumber},{f.DepartureCountry},{f.DepartureAirport},{f.DepartureDateTime},{f.DestinationCountry},{f.DestinationAirport},{f.FlightDuration},{f.PricePerClass[FlightClass.Economy]} / {f.PricePerClass[FlightClass.Business]} / {f.PricePerClass[FlightClass.FirstClass]},{f.Capacity},{f.BookedSeats}"
        ));

        try
        {
            File.WriteAllLines(filePath, lines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save flights: {ex.Message}");
        }
    }
}
