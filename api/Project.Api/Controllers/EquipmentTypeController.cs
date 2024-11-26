using Microsoft.AspNetCore.Mvc;
using Project.Core.DTO.EquipmentDTO;
using Project.Core.Interfaces.IServices.IBusinessServices;

namespace Project.Api.Controllers
{
    public class EquipmentTypeController : BaseApiController
    {
        private readonly IEquipmentTypeService _service;
        public EquipmentTypeController(IEquipmentTypeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<GetEquipmentTypeDTO>> AddEquipmentType(AddEquipmentTypeDTO addEquipmentTypeDTO){
            var newEquipmentType = await _service.AddEquipmentType(addEquipmentTypeDTO);
            return Ok(newEquipmentType);
        }
    }
}