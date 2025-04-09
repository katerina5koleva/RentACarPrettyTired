using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Data.Models;
using RentACar.Data.ViewModels;
using RentACar.Repositories;
using RentACar.Repositories.Interfaces;
using RentACar.Services.Interfaces;

namespace RentACar.Controllers.Controllers
{
    /// <summary>
    /// Controller for managing Auto entities, including listing, creating, editing, and deleting cars.
    /// </summary>
    public class AutoController : Controller
    {
        private readonly IAutoRepository _autoRepository;
        private readonly IPhotoService _photoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoController"/> class.
        /// </summary>
        /// <param name="autoRepository">The repository for managing Auto data.</param>
        /// <param name="photoService">The service for managing photo uploads and deletions.</param>
        public AutoController(IAutoRepository autoRepository, IPhotoService photoService)
        {
            _autoRepository = autoRepository;
            _photoService = photoService;
        }

        /// <summary>
        /// Displays a list of all cars.
        /// </summary>
        /// <returns>A view containing the list of cars.</returns>
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var autos = await _autoRepository.GetAllAutosAsync();
            return View(autos);
        }

        /// <summary>
        /// Displays a list of cars available within a specified date range.
        /// </summary>
        /// <param name="startDate">The start date of the availability period.</param>
        /// <param name="endDate">The end date of the availability period.</param>
        /// <returns>A view containing the list of available cars.</returns>
        public async Task<IActionResult> FreeAutos(DateTime startDate, DateTime endDate)
        {
            var autos = await _autoRepository.GetAllAutosFreeAsync(startDate, endDate);
            return View(autos);
        }

        /// <summary>
        /// Displays the details of a specific car.
        /// </summary>
        /// <param name="id">The ID of the car.</param>
        /// <returns>A view containing the car details, or NotFound if the car does not exist.</returns>
        public async Task<IActionResult> Details(int id)
        {
            Auto auto = await _autoRepository.GetAutoByIdAsync(id);
            if (auto == null)
            {
                return NotFound();
            }
            return View(auto);
        }

        /// <summary>
        /// Displays the form for creating a new car.
        /// </summary>
        /// <returns>A view containing the car creation form.</returns>
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the submission of the car creation form.
        /// </summary>
        /// <param name="autoVM">The view model containing the car data.</param>
        /// <returns>Redirects to the Index view if successful, or redisplays the form with errors.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAutoVM autoVM)
        {
            if (!ModelState.IsValid)
            {
                return View(autoVM);
            }
            var photoResult = await _photoService.AddPhotoAsync(autoVM.Image);
            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(autoVM);
            }
            Auto auto = new Auto
            {
                Brand = autoVM.Brand,
                Model = autoVM.Model,
                Year = autoVM.Year,
                PassengerSeats = autoVM.PassengerSeats,
                Description = autoVM.Description,
                Image = photoResult.Url.ToString(),
                PricePerDay = autoVM.PricePerDay,
            };

            await _autoRepository.AddAsync(auto);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the form for editing an existing car.
        /// </summary>
        /// <param name="id">The ID of the car to edit.</param>
        /// <returns>A view containing the car edit form, or an error view if the car does not exist.</returns>
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            Auto auto = await _autoRepository.GetAutoByIdAsync(id);
            if (auto == null)
            {
                ViewData["ErrorMessage"] = "A problem occurred while proceeding with your request. Access denied.";
                return View("Error");
            }
            EditAutoVM editAutoVM = new EditAutoVM
            {
                Id = auto.Id,
                Brand = auto.Brand,
                Model = auto.Model,
                Year = auto.Year,
                PassengerSeats = auto.PassengerSeats,
                Description = auto.Description,
                URL = auto.Image,
                PricePerDay = auto.PricePerDay,
            };
            return View(editAutoVM);
        }

        /// <summary>
        /// Handles the submission of the car edit form.
        /// </summary>
        /// <param name="editAutoVM">The view model containing the updated car data.</param>
        /// <returns>Redirects to the Index view if successful, or redisplays the form with errors.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAutoVM editAutoVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit idea.");
                return View(editAutoVM);
            }
            Auto auto = await _autoRepository.GetAutoByIdAsync(editAutoVM.Id);
            if (auto == null)
            {
                ViewData["ErrorMessage"] = "A problem occurred while proceeding with your request. Access denied.";
                return View("Error");
            }

            try
            {
                await _photoService.DeletePhotoAsync(auto.Image);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Could not delete photo.");
                return View(editAutoVM);
            }

            var photoResult = await _photoService.AddPhotoAsync(editAutoVM.Image);

            auto.Id = editAutoVM.Id;
            auto.Brand = editAutoVM.Brand;
            auto.Model = editAutoVM.Model;
            auto.Year = editAutoVM.Year;
            auto.PassengerSeats = editAutoVM.PassengerSeats;
            auto.Description = editAutoVM.Description;
            auto.Image = photoResult.Url.ToString();
            auto.PricePerDay = editAutoVM.PricePerDay;

            await _autoRepository.UpdateAsync(auto);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the confirmation page for deleting a car.
        /// </summary>
        /// <param name="id">The ID of the car to delete.</param>
        /// <returns>A view containing the car details, or an error view if the car does not exist.</returns>
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            Auto auto = await _autoRepository.GetAutoByIdAsync(id);
            if (auto == null)
            {
                ViewData["ErrorMessage"] = "A problem occurred while proceeding with your request. Access denied.";
                return View("Error");
            }
            return View(auto);
        }

        /// <summary>
        /// Handles the deletion of a car.
        /// </summary>
        /// <param name="id">The ID of the car to delete.</param>
        /// <returns>Redirects to the Index view if successful, or an error view if the car does not exist.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAuto(int id)
        {
            Auto auto = await _autoRepository.GetAutoByIdAsync(id);
            if (auto == null)
            {
                ViewData["ErrorMessage"] = "A problem occurred while proceeding with your request. Access denied.";
                return View("Error");
            }
            await _autoRepository.DeleteAsync(auto);
            return RedirectToAction("Index");
        }
    }
}
