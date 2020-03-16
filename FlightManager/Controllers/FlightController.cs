using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlightManager.Services.Interfaces;
using FlightManager.Services;
using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FlightManager.Controllers
{
    public class FlightController : Controller
    {
        private IFlightService FlightService;
        private IUserService UserService;
        private IReservationService ReservationService;
        private FlightManagerDbContext context;

        public FlightController(IUserService UserService, IFlightService FlightService, IReservationService ReservationService, FlightManagerDbContext context)
        {
            this.FlightService = FlightService;
            this.UserService = UserService;
            this.ReservationService = ReservationService;
            this.context = context;
        }

        [Authorize(Roles = "admin")]
        public IActionResult CreateFlight()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult
            CreateFlight(string departureLocation,
                        string landingLocation,
                        DateTime departureDateTime,
                        DateTime landingDatetime,
                        string planeType,
                        string planeUniqueId,
                        string pilotName,
                        int regularSeats,
                        int businessSeats)
        {
            if (ModelState.IsValid)
            {
                var currentFlight = FlightService.CreateFlight(departureLocation, landingLocation, departureDateTime, landingDatetime,
                                    planeType, planeUniqueId, pilotName, regularSeats, businessSeats);

                if (currentFlight == -1)
                {
                    ViewBag.Message = "The departure and landing locations can't be the same!";
                    return View();
                }
                if (currentFlight == -2)
                {
                    ViewBag.Message = "The departure datetime can't be ahead of the landing datetime!";
                    return View();
                }
                if (currentFlight == -3)
                {
                    ViewBag.Message = "The plane UNIQUE id must be unique...";
                    return View();
                }
            }
            return this.RedirectToAction("ListFlights", "Flight");
        }

        public IActionResult ListFlights()
        {
            ViewData["ListFlights"] = FlightService.ListFlights();
            return View();
        }

        public IActionResult ListReservations()
        {
            ViewData["ListReservations"] = ReservationService.ListReservations();
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditFlight(int id)
        {
            Flight flight = context.Flights.Find(id);

            EditFlightViewModel model = new EditFlightViewModel
            {
                DepartureLocation = flight.DepartureLocation,
                LandingLocation = flight.LandingLocation,
                DepartureDateTime = flight.DepartureDateTime,
                LandingDateTime = flight.LandingDateTime,
                PlaneType = flight.PlaneType,
                PlaneUniqueId = flight.PlaneUniqueId,
                PilotName = flight.PilotName,
                RegularSeats = flight.RegularSeats,
                BusinessSeats = flight.BusinessSeats
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult EditFlight(EditFlightViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentFlight = FlightService.EditFlight(model.Id, model.DepartureLocation, model.LandingLocation, model.DepartureDateTime, model.LandingDateTime,
                    model.PlaneType, model.PlaneUniqueId, model.PilotName, model.RegularSeats, model.BusinessSeats);

                if (currentFlight == -1)
                {
                    ViewBag.Message = "The departure and landing locations can't be the same!";
                    return View();
                }
                if (currentFlight == -2)
                {
                    ViewBag.Message = "The departure datetime can't be ahead of the landing datetime!";
                    return View();
                }
                if (currentFlight == -3)
                {
                    ViewBag.Message = "The plane UNIQUE id must be unique...";
                    return View();
                }
            }
            return this.RedirectToAction("ListFlights", "Flight");
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteFlight(int id)
        {
            FlightService.DeleteFlight(id);
            return this.RedirectToAction("ListFlights", "Flight");
        }

        public IActionResult ViewFlight(int id, string msg)
        {
            ViewData["Flight"] = FlightService.ViewFlight(id);
            ViewData["Reservations"] = ReservationService.ListReservations();
            ViewBag.Message = msg;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewFlight(int flightId, string feedbackEmail, int regularSeats, int businessSeats)
        {
            if (ModelState.IsValid)
            {
                var currReservation = ReservationService.CreateReservation(flightId, feedbackEmail, regularSeats, businessSeats);

                if (currReservation == -1)
                {
                    return this.RedirectToAction("ViewFlight", "Flight", new { id = flightId, msg = "Not enough regular seats"});
                }
                if (currReservation == -2)
                {
                    return this.RedirectToAction("ViewFlight", "Flight", new { id = flightId, msg = "Not enough business seats" });
                }
            }
            return this.RedirectToAction("ViewFlight", "Flight", new { id = flightId, msg = "Reservation was successfully created!" });
        }
    }
}