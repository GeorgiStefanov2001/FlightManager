using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Services.Interfaces;
using System.Linq;
using FlightManager.ViewModels;
using System.Net.Mail;
using System.Net;

namespace FlightManager.Services
{
    public class ReservationService : IReservationService
    {
        private FlightManagerDbContext context;
        public ReservationService(FlightManagerDbContext context)
        {
            this.context = context;
        }

        public int CreateReservation(int flightId, string feedbackEmail, int regularSeats, int businessSeats)
        {
            var flight = context.Flights.Find(flightId);

            if (flight.RegularSeats < regularSeats)
            {
                //not enough regular seats
                return -1;
            }
            if (flight.BusinessSeats < businessSeats)
            {
                //not enough business seats
                return -2;
            }

            var reservation = new Reservation
            {
                FlightId = flightId,
                FeedBackEmail = feedbackEmail,
                RegularSeats = regularSeats,
                BusinessSeats = businessSeats
            };

            flight.RegularSeats -= regularSeats;
            flight.BusinessSeats -= businessSeats;

            string emailBody = $"Reservation about flight {flightId} (Plane id: {flight.PlaneUniqueId}). " +
                $"You reserved {regularSeats} regular seats and {businessSeats} business seats.";

            //The feedback mail that the user enters must be valid!!!!!
            SendMail(feedbackEmail, emailBody);

            context.Reservations.Add(reservation);
            context.SaveChanges();

            return reservation.Id;
        }

        public ListReservationsViewModel ListReservations()
        {
            var reservations = context.Reservations.Select(j => new CreateReservationViewModel()
            {
                Id = j.Id,
                FlightId = j.FlightId,
                FeedBackEmail = j.FeedBackEmail,
                RegularSeats = j.RegularSeats,
                BusinessSeats = j.BusinessSeats
            });

            var model = new ListReservationsViewModel() { Reservations = reservations };

            return model;
        }

        protected void SendMail(string feedbackMail, string body)
        {
            MailMessage message = new MailMessage("georgi.flight.manager@gmail.com", feedbackMail);
            message.Subject = "Reservation information";
            message.Body = body;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential {
                UserName = "georgi.flight.manager@gmail.com",
                Password = "flightmangerpass123"
            };

            smtpClient.EnableSsl = true;
            smtpClient.Send(message);

        }
    }
}
