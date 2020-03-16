using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data.Models;
using FlightManager.ViewModels;

namespace FlightManager.Services.Interfaces
{
    public interface IFlightService
    {
        int CreateFlight (string departureLocation,
                            string landingLocation,
                            DateTime departureDateTime,
                            DateTime landingDatetime,
                            string planeType,
                            string planeUniqueId,
                            string pilotName,
                            int regularSeats,
                            int businessSeats);

        ListFlightsViewModel ListFlights();

        Flight ViewFlight(int id);

        int EditFlight(int id,
                        string departureLocation,
                        string landingLocation,
                        DateTime departureDateTime,
                        DateTime landingDatetime,
                        string planeType,
                        string planeUniqueId,
                        string pilotName,
                        int regularSeats,
                        int businessSeats);

        void DeleteFlight(int id);
    }
}
