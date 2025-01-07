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
        public async Task<ActionResult<List<GetReservationDTO>>> GetAllReservations()
        {
            return Ok(await _service.GetAll());
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
        public async Task<ActionResult<SuccessResponseDTO>> DeleteReservation(string id)
        {
            await _service.Delete(id);
            return Ok(new SuccessResponseDTO("Reserwacja anulowana pomyślnie"));
        }
    }
}