using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentACar.Data.Models;
using RentACar.Data.ViewModels;
using RentACar.Repositories;
using RentACar.Repositories.Interfaces;
using System.Security.Claims;

namespace RentACar.Controllers
{
    /// <summary>
    /// The RequestController handles all operations related to requests in the RentACar application.
    /// It includes actions for creating, viewing, approving, declining, and deleting requests.
    /// </summary>
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IAutoRepository _autoRepository;
        private readonly IBookingPeriodRepository _bookingPeriodRepository;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the RequestController class.
        /// </summary>
        /// <param name="requestRepository">The request repository for managing request data.</param>
        /// <param name="autoRepository">The auto repository for managing vehicle data.</param>
        /// <param name="bookingPeriodRepository">The booking period repository for managing booking data.</param>
        /// <param name="logger">The logger for logging errors and information.</param>

        public RequestController(IRequestRepository requestRepository, IAutoRepository autoRepository, IBookingPeriodRepository bookingPeriodRepository, ILogger<HomeController> logger)
        {
            _requestRepository = requestRepository;
            _autoRepository = autoRepository;
            _bookingPeriodRepository = bookingPeriodRepository;
            _logger = logger;
        }

        /// <summary>
        /// Displays a list of all requests with optional filtering and sorting.
        /// </summary>
        /// <param name="filter">The filter to apply (e.g., "all", "pending", "processed").</param>
        /// <param name="sortOrder">The sort order to apply (e.g., "newest", "oldest", "status").</param>
        /// <returns>A view displaying the filtered and sorted list of requests.</returns>
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index(string filter = "all", string sortOrder = "newest")
        {
            IEnumerable<Request> requests;


            switch (filter.ToLower())
            {
                case "pending":
                    requests = await _requestRepository.GetAllRequestsUnanswered();
                    break;
                case "processed":
                    requests = await _requestRepository.GetAllRequestsAnswered();
                    break;
                default:
                    requests = await _requestRepository.GetAllRequestsAsync();
                    break;
            }


            requests = sortOrder.ToLower() switch
            {
                "oldest" => requests.OrderBy(r => r.DateOfRequest),
                "status" => requests.OrderBy(r => r.IsApproved),
                _ => requests.OrderByDescending(r => r.DateOfRequest)
            };


            ViewBag.CurrentFilter = filter;
            ViewBag.CurrentSort = sortOrder;

            return View(requests);
        }

        /// <summary>
        /// Displays a list of requests created by the currently logged-in user.
        /// </summary>
        /// <returns>A view displaying the user's requests.</returns>
        [Authorize(Roles = "BasicUser")]
        public async Task<IActionResult> MyRequests()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requests = await _requestRepository.GetAllByUserId(userId);
            return View(requests);
        }

        /// <summary>
        /// Displays the details of a specific request.
        /// </summary>
        /// <param name="id">The ID of the request to view.</param>
        /// <returns>A view displaying the request details, or an error if the request is not found or access is denied.</returns>
        public async Task<IActionResult> Details(int id)
        {
            var request = await _requestRepository.GetRequestByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Administrator"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (request.UserId != userId)
                {
                    return Forbid();
                }
            }

            return View(request);
        }

        /// <summary>
        /// Displays the form for creating a new request.
        /// </summary>
        /// <returns>A view displaying the request creation form.</returns>
        [Authorize(Roles = "BasicUser")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateRequestVM
            {
                PickUpDate = DateTime.Today.AddDays(1),
                ReturnDate = DateTime.Today.AddDays(2)
            };
            return View(model);
        }

        /// <summary>
        /// Checks the availability of vehicles for the specified pick-up and return dates.
        /// </summary>
        /// <param name="pickUpDate">The pick-up date.</param>
        /// <param name="returnDate">The return date.</param>
        /// <returns>A partial view displaying available vehicles, or an error if the dates are invalid.</returns>
        [Authorize(Roles = "BasicUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckAvailability([FromForm] DateTime pickUpDate, [FromForm] DateTime returnDate)
        {
            try
            {

                if (pickUpDate == default || returnDate == default)
                {
                    return BadRequest("Both dates are required");
                }

                if (pickUpDate.Date < DateTime.Today)
                {
                    return BadRequest("Pick-up date cannot be in the past");
                }

                if (pickUpDate >= returnDate)
                {
                    return BadRequest("Return date must be after pick-up date");
                }


                var availableAutos = await _autoRepository.GetAllAutosFreeAsync(pickUpDate.Date, returnDate.Date);


                var model = new CreateRequestVM
                {
                    AvailableAutos = availableAutos,
                    PickUpDate = pickUpDate,
                    ReturnDate = returnDate
                };

                return PartialView("_AutoSelectionPartial", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckAvailability");
                return StatusCode(500, "An error occurred while checking availability");
            }
        }

        /// <summary>
        /// Creates a new request based on the provided data.
        /// </summary>
        /// <param name="model">The data for the new request.</param>
        /// <returns>A redirect to the user's requests if successful, or the creation form with errors if validation fails.</returns>
        [Authorize(Roles = "BasicUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRequestVM model)
        {
            if (!ModelState.IsValid)
                return View(model);


            var isAvailable = await _bookingPeriodRepository.IsAutoAvailableAsync(
                model.AutoId,
                model.PickUpDate,
                model.ReturnDate);

            if (!isAvailable)
            {
                ModelState.AddModelError("", "Selected vehicle is no longer available.");
                model.AvailableAutos = await _autoRepository.GetAllAutosFreeAsync(model.PickUpDate, model.ReturnDate);
                return View(model);
            }

            if (model.AutoId == 0)
            {
                ModelState.AddModelError("", "Please select a vehicle.");
                model.AvailableAutos = await _autoRepository.GetAllAutosFreeAsync(model.PickUpDate, model.ReturnDate);
                return View(model);
            }


            var request = new Request
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                AutoId = model.AutoId,
                PickUpDate = model.PickUpDate,
                ReturnDate = model.ReturnDate,
                DateOfRequest = DateTime.Now
            };

            await _requestRepository.AddAsync(request);
            TempData["SuccessMessage"] = "Request confirmed successfully!";
            return RedirectToAction("MyRequests");
        }

        /// <summary>
        /// Approves a specific request.
        /// </summary>
        /// <param name="id">The ID of the request to approve.</param>
        /// <returns>A redirect to the request list with a success or error message.</returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var success = await _requestRepository.ApproveRequest(id);
            if (!success)
            {
                TempData["ErrorMessage"] = "Failed to approve request. The auto might be unavailable for the selected period.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Request approved successfully";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Declines a specific request.
        /// </summary>
        /// <param name="id">The ID of the request to decline.</param>
        /// <returns>A redirect to the request list with a success or error message.</returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Decline(int id)
        {
            var success = await _requestRepository.DeclineRequest(id);
            if (!success)
            {
                TempData["ErrorMessage"] = "Failed to decline request";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Request declined successfully";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the confirmation page for deleting a specific request.
        /// </summary>
        /// <param name="id">The ID of the request to delete.</param>
        /// <returns>A view displaying the delete confirmation page, or an error if the request is not found or access is denied.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var request = await _requestRepository.GetRequestByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (request.UserId != userId && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }

            return View(request);
        }

        /// <summary>
        /// Deletes a specific request after confirmation.
        /// </summary>
        /// <param name="id">The ID of the request to delete.</param>
        /// <returns>A redirect to the user's requests with a success or error message.</returns>
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _requestRepository.GetRequestByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            var success = await _requestRepository.DeleteAsync(request);
            if (!success)
            {
                TempData["ErrorMessage"] = "Failed to delete request";
                return View(request);
            }

            TempData["SuccessMessage"] = "Request deleted successfully";
            return RedirectToAction(nameof(MyRequests));
        }
    }
}