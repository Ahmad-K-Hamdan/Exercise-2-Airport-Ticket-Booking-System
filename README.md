# Airport Ticket Booking System

## Objective

This project is a C# console application designed to simulate an airport ticket booking system. The system allows passengers to book tickets, search for flights, and manage their bookings, while providing managers with tools to oversee bookings and upload flight data.

---

## Features

### For Passengers

- **Book a Flight**
  - Select flights using various search parameters.
  - Choose flight class: Economy, Business, First Class (prices vary by class).

- **Search Available Flights**
  - Search using:
    - Price
    - Departure Country
    - Destination Country
    - Departure Date
    - Departure Airport
    - Arrival Airport
    - Class

- **Manage Bookings**
  - Cancel bookings
  - Modify bookings
  - View personal bookings

---

### For Managers

- **Filter Bookings**
  - Search/filter bookings by:
    - Flight
    - Price
    - Departure Country
    - Destination Country
    - Departure Date
    - Departure Airport
    - Arrival Airport
    - Passenger
    - Class

- **Batch Flight Upload**
  - Import flights using a CSV file.

- **Validate Imported Flight Data**
  - Model-level validations on flight data.
  - Detailed error reports for issues in imported files.

- **Dynamic Model Validation Details**
  - Display dynamically generated validation constraints for each flight data field.
  - Example:
    - **Departure Country:** Free Text, Required
    - **Departure Date:** DateTime, Required, Range: today → future

---

## Data Storage

- The application uses the file system to store all data (no database required).

---

## Notes

- The codebase adheres to established C# programming conventions and best practices.
- The project structure is designed for readability and maintainability.

---

## Getting Started

1. **Requirements**
    - .NET SDK (recommend version 6.0 or higher)
2. **Build & Run**
    - Clone the repository
    - Navigate to the project directory
    - Run:
      ```bash
      dotnet build
      dotnet run
      ```
3. **Usage**
    - Follow the interactive console prompts to book flights, manage bookings, or, as a manager, upload and validate flight data.
