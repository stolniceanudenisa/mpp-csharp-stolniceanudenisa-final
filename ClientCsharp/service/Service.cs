using System;
using System.Collections.Generic;
using mpp_csharp_stolniceanudenisa_final.model;
using ClientCsharp.persistence;

namespace ClientCsharp.service
{
    public class Service
    {
        
    public IUserRepository UserRepository { get; set; }
    public IBookingRepository BookingRepository { get; set; }
    public IClientRepository ClientRepository { get; set; }
    public IFlightRepository FlightRepository { get; set; }

    public Service(IUserRepository userRepository, IBookingRepository bookingRepository, IClientRepository clientRepository, IFlightRepository flightRepository)
    {
        UserRepository = userRepository;
        BookingRepository = bookingRepository;
        ClientRepository = clientRepository;
        FlightRepository = flightRepository;
    }

    public bool validateLogin(string username, string password)
    {
        return UserRepository.ExistsUser(username, password);
    }

    public IEnumerable<Flight> findFlightByDestinationAndDate(string destination, DateTime dateTime)
    {
        return FlightRepository.FindFlightByDestinationAndDate(destination, dateTime);
    }

    public int getNumberOfBookingsForFlight(long flightId)
    {
        var bookings = BookingRepository.GetAll();

        int count = 0;
        foreach(var book in bookings) 
        { 
            if (book.Flight.id == flightId)
            {
                count += book.Passengers.Count + 1;
            }
        }

        return count;
    }

    public IEnumerable<Flight> getAllFlights()
    {
        return FlightRepository.GetAll();
    }
    
    public IEnumerable<User> getAllUsers()
    {
        return UserRepository.GetAll();
    }

    public bool bookFlight(long flightId, string clientName, string clientAddress, List<string> clients)
    {
        try
        {
            Client client = new Client(clientName, clientAddress);
            client.id = ClientRepository.GetLowestAvailableId() + 1;
            ClientRepository.Add(client);
            Booking booking = new Booking(FlightRepository.findOne(flightId), client, clients);
            booking.id = BookingRepository.GetLowestAvailableId() + 1;
            BookingRepository.Add(booking);
            return true; // Booking was successful
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            return false; // Booking failed
        }
    }
    
    // public bool bookFlight(long flightId, string clientName, string clientAddress, List<string> clients)
    // {
    //     Client client = new Client(clientName, clientAddress);
    //     client.id = ClientRepository.GetLowestAvailableId() + 1;
    //     ClientRepository.Add(client);
    //     Booking booking = new Booking(FlightRepository.findOne(flightId), client, clients);
    //     booking.id = BookingRepository.GetLowestAvailableId() + 1;
    //     return BookingRepository.Add(booking) == null;
    // }

    public Flight getFlightById(long flightId)
    {
        return FlightRepository.findOne(flightId);
    }

        
    }
}