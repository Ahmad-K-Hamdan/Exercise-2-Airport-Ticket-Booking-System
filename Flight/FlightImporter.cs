using Airport_Ticket_Booking_System;
using Airport_Ticket_Booking_System.Enums;

public static class FlightImporter
{
    public static List<Flight> ImportFlights()
    {
        var lines = FlightImporter.ReadFlightData();
        var parsedData = FlightImporter.ParseFlightData(lines);
        return FlightImporter.CreateFlights(parsedData);
    }

    private static List<string> ReadFlightData(string filePath = "Data.csv")
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Data file not found.", filePath);

        try
        {
            var lines = File.ReadAllLines(filePath).Skip(1).ToList();
            return lines;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return new List<string>();
        }
    }

    private static List<string[]> ParseFlightData(IEnumerable<string> lines)
    {
        {
            var parsed = new List<string[]>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                var columns = line.Split(',');
                parsed.Add(columns);
            }

            return parsed;
        }
    }

    private static List<Flight> CreateFlights(List<string[]> lines)
    {
        var ListOfFlights = new List<Flight>();

        foreach (var line in lines)
        {
            try
            {
                var econ = line[7].Split('/');
                var prices = new Dictionary<FlightClass, decimal>
                {
                    { FlightClass.Economy, decimal.Parse(econ[0]) },
                    { FlightClass.Business, decimal.Parse(econ[1]) },
                    { FlightClass.FirstClass, decimal.Parse(econ[2]) },
                };

                var errors = FlightValidator.GetValidationErrors(
                    flightNumber: line[0],
                    departureCountry: line[1],
                    departureAirport: line[2],
                    departureDateTime: DateTime.Parse(line[3]),
                    destinationCountry: line[4],
                    destinationAirport: line[5],
                    flightDuration: TimeSpan.Parse(line[6]),
                    pricePerClass: prices,
                    capacity: int.Parse(line[8]),
                    bookedSeats: int.Parse(line[9])
                );

                if (errors.Count > 0)
                {
                    Console.WriteLine(
                        $"Validation errors for flight {line[0]}: {string.Join("; ", errors)}"
                    );
                    continue;
                }

                var flight = new Flight(
                    line[0],
                    line[1],
                    line[2],
                    DateTime.Parse(line[3]),
                    line[4],
                    line[5],
                    TimeSpan.Parse(line[6]),
                    prices,
                    int.Parse(line[8]),
                    int.Parse(line[9])
                );
                ListOfFlights.Add(flight);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Failed to parse line: {string.Join(",", line)}\nError: {ex.Message}"
                );
            }
        }

        return ListOfFlights;
    }
}
