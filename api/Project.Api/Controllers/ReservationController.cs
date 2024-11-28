using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<GetReservationDTO>>> GetAllReservations()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<GetReservationDTO>> MakeReservation(AddReservationDTO addReservationDTO)
        {
            var reservation = await _service.Create(addReservationDTO);
            return Ok(reservation);
        }
    }
}