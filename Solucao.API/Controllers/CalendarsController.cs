using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Solucao.Application.Contracts;
using Solucao.Application.Contracts.Requests;
using Solucao.Application.Data.Entities;
using Solucao.Application.Service.Interfaces;
using Solucao.Application.Utils;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Solucao.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class CalendarsController : ControllerBase
    {
        private readonly ICalendarService calendarService;
        private readonly IUserService userService;
        private readonly ILogger<CalendarsController> logger;

        public CalendarsController(ICalendarService _calendarService, IUserService _userService, ILogger<CalendarsController> _logger)
        {
            calendarService = _calendarService;
            userService = _userService;
            logger = _logger;
        }

        [HttpGet("calendar")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Calendar))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IEnumerable<EquipamentList>> GetAllAsync([FromQuery] CalendarRequest model)
        {
            logger.LogInformation($"{nameof(CalendarsController)} -{nameof(GetAllAsync)} | Inicio da chamada");
            return await calendarService.GetAllByDate(model.Date);
        }

        [HttpGet("calendar/availability")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Calendar))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IEnumerable<CalendarViewModel>> AvailabilityAsync([FromQuery] CalendarRequest model)
        {
            logger.LogInformation($"{nameof(CalendarsController)} -{nameof(AvailabilityAsync)} | Inicio da chamada");
            return await calendarService.Availability(model.StartDate, model.EndDate, model.ClientId, model.EquipamentId, model.DriverId, model.TechniqueId);
        }

        [HttpPost("calendar")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Calendar))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IActionResult> PostAsync([FromBody] CalendarViewModel model)
        {
            logger.LogInformation($"{nameof(CalendarsController)} - {nameof(PostAsync)} | Inicio da chamada");
            ValidationResult result;
            result = await calendarService.ValidateLease(model.Date, model.ClientId, model.EquipamentId, model.CalendarSpecifications, model.StartTime1, model.EndTime1);

            if (result != null)
            {
                logger.LogWarning($"{nameof(CalendarsController)} -{nameof(PostAsync)} | Erro na criacao - {result}");
                if (!result.ErrorMessage.Contains("minutos"))
                    return NotFound(result);
                else
                    model.Note += result.ErrorMessage;
            }
            
            var user = await userService.GetByName(User.Identity.Name);

            result = await calendarService.Add(model, user.Id);

            if (result != null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("calendar/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Calendar))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IActionResult> PutAsync([FromBody] CalendarViewModel model)
        {
            ValidationResult result;
            result = await calendarService.ValidateLease(model.Date, model.ClientId, model.EquipamentId, model.CalendarSpecifications, model.StartTime1, model.EndTime1);

            if (result != null)
            {
                if (!result.ErrorMessage.Contains("minutos"))
                    return NotFound(result);
                else
                    model.Note += result.ErrorMessage;
            }

            var user = await userService.GetByName(User.Identity.Name);

            result = await calendarService.Update(model, user.Id);

            if (result != null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("calendar/update-driver-or-technique-calendar")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Calendar))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IActionResult> UpdateDriverOrTechniqueCalendarAsync([FromBody] CalendarRequest model)
        {
            ValidationResult result;
            result = await calendarService.UpdateDriverOrTechniqueCalendar(model.CalendarId.Value, model.PersonId.Value, model.IsDriver);

            if (result != null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("calendar/update-status-or-travel-on-calendar")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Calendar))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IActionResult> UpdateStatusOrTravelOnCalendarAsync([FromBody] CalendarRequest model)
        {
            ValidationResult result;
            result = await calendarService.UpdateStatusOrTravelOnCalendar(model.CalendarId.Value, model.Status, model.TravelOn, model.IsTravelOn);

            if (result != null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("calendar/update-contract-made")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Calendar))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IActionResult> UpdateContractMadeAsync([FromBody] CalendarRequest model)
        {
            ValidationResult result;
            result = await calendarService.UpdateContractMade(model.CalendarId.Value);

            if (result != null)
                return NotFound(result);

            return Ok(result);
        }
    }
}
