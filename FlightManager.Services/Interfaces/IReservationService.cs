using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data.Models;
using FlightManager.ViewModels;

namespace FlightManager.Services.Interfaces
{
    public interface IReservationService
    {
        int CreateReservation(int flightId, string feedbackEmail, int regularSeats, int businessSeats);

        ListReservationsViewModel ListReservations();
    }
}
