using AppointmentScheduler.Models.ViewModels;
using AppointmentScheduler.Services;
using AppointmentScheduler.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using AppointmentScheduler.Utility;
using System;

namespace AppointmentScheduler.Controllers.Api
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;

        public AppointmentApiController(IAppointmentService appointmentService, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        }
        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentVM data)
        {
           CommonResponsecs<int> commonResponsecs = new CommonResponsecs<int>();
            try {
                commonResponsecs.status = _appointmentService.AddUpdate(data).Result;
                if (commonResponsecs.status == 1) {
                    commonResponsecs.message = Helper.appointmentUpdated;
                }
                if (commonResponsecs.status == 2)
                {
                    commonResponsecs.message = Helper.appointmentAdded;
                }
            } catch (Exception e)
            {
                commonResponsecs.message = e.Message;
                commonResponsecs.status = Helper.failure_code;
            }


            return Ok(commonResponsecs);
        }
    }
}
