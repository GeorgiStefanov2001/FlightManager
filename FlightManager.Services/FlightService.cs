using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Services.Interfaces;
using System.Linq;
using FlightManager.ViewModels;

namespace FlightManager.Services
{
    public class FlightService : IFlightService
    {
        private FlightManagerDbContext context;
        public FlightService(FlightManagerDbContext context)
        {
            this.context = context;
        }
        public int CreateFlight(string departureLocation,
                                string landingLocation,
                                DateTime departureDateTime,
                                DateTime landingDatetime,
                                string planeType,
                                string planeUniqueId,
                                string pilotName,
                                int regularSeats,
                                int businessSeats)
        {

            if (departureLocation == landingLocation) return -1; //both locations cant be the same

            if (DateTime.Compare(departureDateTime, landingDatetime) > 0) return -2; //the departure cant be ahead of the landing

            var flights = context.Flights.FirstOrDefault(x => x.PlaneUniqueId == planeUniqueId);
            if (flights != null) return -3; //the plane id should be unique

            var flight = new Flight
            {
                DepartureLocation = departureLocation,
                LandingLocation = landingLocation,
                DepartureDateTime = departureDateTime,
                LandingDateTime = landingDatetime,
                PlaneType = planeType,
                PlaneUniqueId = planeUniqueId,
                PilotName = pilotName,
                RegularSeats = regularSeats,
                BusinessSeats = businessSeats
            };

            context.Flights.Add(flight);
            context.SaveChanges();

            return flight.Id;
        }

        public ListFlightsViewModel ListFlights()
        {
            var flights = context.Flights.Select(j => new CreateFlightViewModel()
            {
                Id = j.Id,
                DepartureLocation = j.DepartureLocation,
                LandingLocation = j.LandingLocation,
                DepartureDateTime = j.DepartureDateTime,
                LandingDateTime = j.LandingDateTime,
                PlaneType = j.PlaneType,
                PlaneUniqueId = j.PlaneUniqueId,
                PilotName = j.PilotName,
                RegularSeats = j.RegularSeats,
                BusinessSeats = j.BusinessSeats
            });

            var model = new ListFlightsViewModel() { Flights = flights };

            return model;
        }

        public Flight ViewFlight(int id)
        {
            return context.Flights.Find(id);
        }

        public int EditFlight(int id,
                                string departureLocation,
                                string landingLocation,
                                DateTime departureDateTime,
                                DateTime landingDatetime,
                                string planeType,
                                string planeUniqueId,
                                string pilotName,
                                int regularSeats,
                                int businessSeats)
        {

            if (departureLocation == landingLocation) return -1; //both locations cant be the same

            if (DateTime.Compare(departureDateTime, landingDatetime) > 0) return -2; //the departure cant be ahead of the landing

            var testFlight = context.Flights.FirstOrDefault(x => x.PlaneUniqueId == planeUniqueId);
            if (testFlight != null && testFlight.Id!=id) return -3; //the plane id should be unique

            var flight = context.Flights.Find(id);

            flight.Id = id;
            flight.DepartureLocation = departureLocation;
            flight.LandingLocation = landingLocation;
            flight.DepartureDateTime = departureDateTime;
            flight.LandingDateTime = landingDatetime;
            flight.PlaneType = planeType;
            flight.PlaneUniqueId = planeUniqueId;
            flight.PilotName = pilotName;
            flight.RegularSeats = regularSeats;
            flight.BusinessSeats = businessSeats;

            context.Update(flight);
            context.SaveChanges();

            return flight.Id;
        }

        public void DeleteFlight(int id)
        {
            var flight = context.Flights.Find(id);

            if (flight != null)
            {
                context.Remove(flight);
                context.SaveChanges();
            }
        }
    }
}
