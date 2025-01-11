using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTO.BaseDTO;
using Project.Core.DTO.EquipmentDTO;
using Project.Core.DTO.ReservationsDTO;
using Project.Core.Interfaces.IServices.IBusinessServices;

namespace Project.Api.Controllers
{
    public class ReservationController : BaseApiController
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Policy = "Users")] 
        public async Task<ActionResult<List<GetReservationDTO>>> GetAllReservations(DateTime? startDate = null, DateTime? endDate = null, DateTime? specificDate = null, string lastNamePartial = null, string reservationId = null)
        {
            return Ok(await _service.GetFilteredData(startDate, endDate, specificDate, lastNamePartial, reservationId));
        }

        [HttpPost]
        public async Task<ActionResult<GetReservationDTO>> MakeReservation(AddReservationDTO addReservationDTO)
        {
            var reservation = await _service.Create(addReservationDTO);
            return Ok(reservation);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetReservationDTO>> GetReservation(string id)
        {
            var reservation = await _service.GetById(id);
            return Ok(reservation);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Users")] 
        public async Task<ActionResult<SuccessResponseDTO>> DeleteReservation(string id)
        {
            await _service.Delete(id);
            return Ok(new SuccessResponseDTO("Reserwacja anulowana pomyślnie"));
        }
    }
}