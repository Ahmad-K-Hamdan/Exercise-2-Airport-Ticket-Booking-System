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
                    var flightNumber = columns[0].Trim();
                    var departureCountry = columns[1].Trim();
                    var departureAirport = columns[2].Trim();
                    var destinationCountry = columns[4].Trim();
                    var destinationAirport = columns[5].Trim();

                    if (!DateTime.TryParse(columns[3], CultureInfo.InvariantCulture, DateTimeStyles.None, out var departureDateTime))
                    {
                        Console.WriteLine($"Invalid departure date '{columns[3]}' for flight {flightNumber}.");
                        continue;
                    }

                    if (!TimeSpan.TryParse(columns[6], out var flightDuration))
                    {
                        Console.WriteLine($"Invalid flight duration '{columns[6]}' for flight {flightNumber}.");
                        continue;
                    }

                    var priceParts = columns[7].Split('/');
                    if (priceParts.Length != 3
                        || !decimal.TryParse(priceParts[0].Trim(), out var economyPrice)
                        || !decimal.TryParse(priceParts[1].Trim(), out var businessPrice)
                        || !decimal.TryParse(priceParts[2].Trim(), out var firstClassPrice))
                    {
                        Console.WriteLine($"Invalid price format for flight {flightNumber}.");
                        continue;
                    }

                    var prices = new Dictionary<FlightClass, decimal>
                    {
                        { FlightClass.Economy, economyPrice },
                        { FlightClass.Business, businessPrice },
                        { FlightClass.FirstClass, firstClassPrice }
                    };

                    if (!int.TryParse(columns[8], out var capacity))
                    {
                        Console.WriteLine($"Invalid capacity '{columns[8]}' for flight {flightNumber}.");
                        continue;
                    }

                    if (!int.TryParse(columns[9], out var bookedSeats))
                    {
                        Console.WriteLine($"Invalid booked seats '{columns[9]}' for flight {flightNumber}.");
                        continue;
                    }

                    var flight = FlightFactory.CreateFlight(
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

                    var errors = FlightValidator.GetValidationErrors(flight);
                    if (errors.Count > 0)
                    {
                        Console.WriteLine($"Validation errors for flight {flightNumber}: {string.Join("; ", errors)}");
                        continue;
                    }

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
