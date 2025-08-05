using System;
using System.Collections.Generic;
using Airport_Ticket_Booking_System.Enums;

namespace Airport_Ticket_Booking_System
{
    public class FlightSearchService
    {
        private readonly object[] UserCriteria = new object[9];

        public void ShowCriteriaMenu()
        {
            Console.WriteLine("\nSelect filtering criteria:");
            Console.WriteLine("1. Price");
            Console.WriteLine("2. Departure Country");
            Console.WriteLine("3. Departure Airport");
            Console.WriteLine("4. Destination Country");
            Console.WriteLine("5. Destination Airport");
            Console.WriteLine("6. Departure Date");
            Console.WriteLine("8. Confirm & Show Flights");
            Console.WriteLine("0. Reset filtering criteria");
            Console.Write("Enter your choice: ");
        }

        public bool HandleCriteriaMenuInput()
        {
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    HandlePriceInput();
                    break;

                case "2":
                    HandleDepartureCountryInput();
                    break;

                case "3":
                    HandleDepartureAirportInput();
                    break;

                case "4":
                    HandleDestinationCountryInput();
                    break;

                case "5":
                    HandleDestinationAirportInput();
                    break;

                case "6":
                    HandleDepartureDateInput();
                    break;

                case "8":
                    return true;

                case "0":
                    ResetCriteria();
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose a number between 0 and 8.");
                    break;
            }

            return false;
        }

        public List<Flight> FilterFlights(List<Flight> flights)
        {
            IEnumerable<Flight> query = flights;

            if (UserCriteria[0] is decimal maxPrice)
            {
                query = query.Where(flight =>
                    flight.PricePerClass.Values.Any(price => price <= maxPrice)
                );
            }

            if (UserCriteria[1] is string depCountry)
            {
                query = query.Where(flight =>
                    flight.DepartureCountry.Equals(depCountry, StringComparison.OrdinalIgnoreCase)
                );
            }

            if (UserCriteria[2] is string depAirport)
            {
                query = query.Where(flight =>
                    flight.DepartureAirport.Equals(depAirport, StringComparison.OrdinalIgnoreCase)
                );
            }

            if (UserCriteria[3] is string destCountry)
            {
                query = query.Where(flight =>
                    flight.DestinationCountry.Equals(
                        destCountry,
                        StringComparison.OrdinalIgnoreCase
                    )
                );
            }

            if (UserCriteria[4] is string destAirport)
            {
                query = query.Where(flight =>
                    flight.DestinationAirport.Equals(
                        destAirport,
                        StringComparison.OrdinalIgnoreCase
                    )
                );
            }

            if (UserCriteria[5] is DateTime depDate)
            {
                query = query.Where(flight => flight.DepartureDateTime.Date == depDate.Date);
            }

            return query.ToList();
        }

        private void HandlePriceInput()
        {
            while (true)
            {
                Console.Write("Enter max price: ");
                string? priceInput = Console.ReadLine();
                if (decimal.TryParse(priceInput, out decimal price) && price >= 0)
                {
                    UserCriteria[0] = price;
                    break;
                }
                Console.WriteLine("Invalid price. Please enter a valid positive decimal number.");
            }
        }

        private void HandleDepartureCountryInput()
        {
            while (true)
            {
                Console.Write("Enter departure country: ");
                string? input = Console.ReadLine();
                try
                {
                    UserCriteria[1] = FlightValidator.ValidateCountry(input!);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nInvalid country: {ex.Message}");
                }
            }
        }

        private void HandleDepartureAirportInput()
        {
            while (true)
            {
                Console.Write("Enter departure airport: ");
                string? input = Console.ReadLine();
                try
                {
                    UserCriteria[2] = FlightValidator.ValidateAirport(input!);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nInvalid airport: {ex.Message}");
                }
            }
        }

        private void HandleDestinationCountryInput()
        {
            while (true)
            {
                Console.Write("Enter destination country: ");
                string? input = Console.ReadLine();
                try
                {
                    UserCriteria[3] = FlightValidator.ValidateCountry(input!);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nInvalid country: {ex.Message}");
                }
            }
        }

        private void HandleDestinationAirportInput()
        {
            while (true)
            {
                Console.Write("Enter destination airport: ");
                string? input = Console.ReadLine();
                try
                {
                    UserCriteria[4] = FlightValidator.ValidateAirport(input!);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nInvalid airport: {ex.Message}");
                }
            }
        }

        private void HandleDepartureDateInput()
        {
            DateTime current = UserCriteria[5] is DateTime datetime ? datetime : DateTime.MinValue;
            DateTime newDate = current.Date;

            while (true)
            {
                Console.Write("Enter departure date (yyyy-MM-dd): ");
                string? dateInput = Console.ReadLine();

                if (DateTime.TryParse(dateInput, out DateTime parsedDate))
                {
                    newDate = parsedDate.Date;
                    break;
                }
                Console.WriteLine("Invalid date format.");
            }

            UserCriteria[5] = newDate;
        }

        private void ResetCriteria()
        {
            for (int i = 0; i < UserCriteria.Length; i++)
            {
                UserCriteria[i] = null!;
            }
            Console.WriteLine("\nAll search criteria have been reset.");
        }
    }
}
