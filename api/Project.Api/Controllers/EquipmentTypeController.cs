using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTO.BaseDTO;
using Project.Core.DTO.EquipmentDTO;
using Project.Core.DTO.ReservationsDTO;
using Project.Core.Interfaces.IServices.IBusinessServices;

namespace Project.Api.Controllers
{
    public class EquipmentTypeController : BaseApiController
    {
        private readonly IEquipmentTypeService _service;
        private readonly IReservationEquipmentService _reservationsEquipmentService;

        public EquipmentTypeController(IEquipmentTypeService service, IReservationEquipmentService reservationsEquipmentService)
        {
            _service = service;
            _reservationsEquipmentService = reservationsEquipmentService;
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")] 
        public async Task<ActionResult<GetEquipmentTypeDTO>> AddEquipmentType(AddEquipmentTypeDTO addEquipmentTypeDTO)
        {
            var newEquipmentType = await _service.Create(addEquipmentTypeDTO);
            return Ok(newEquipmentType);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")] 
        public async Task<ActionResult<SuccessResponseDTO>> DeleteEquipmentType(string id)
        {
            await _service.Delete(id);
            return Ok(new SuccessResponseDTO("Operacja wykonana prawidłowo"));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")] 
        public async Task<ActionResult<GetEquipmentTypeDTO>> UpdateEquipmentType(AddEquipmentTypeDTO equipmentTypeDTO, string id)
        {
            var updatedData = await _service.Update(equipmentTypeDTO, id);
            return Ok(updatedData);
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")] 
        public async Task<ActionResult<GetEquipmentTypeDTO>> GetAllEquipment()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")] 
        public async Task<ActionResult<GetEquipmentTypeDTO>> GetSingle(string id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPost("avaiableEquipment")]
        public async Task<ActionResult<List<GetEquipmentTypeDTO>>> GetAvailableEquipment(ReservationDetailsDTO reservationDetailsDTO)
        {
            var availableEquipment = await _reservationsEquipmentService.FetchAvaiableEquipment(reservationDetailsDTO);
            return Ok(availableEquipment);
        }

    }
}