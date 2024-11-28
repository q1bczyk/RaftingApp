using Microsoft.AspNetCore.Mvc;
using Project.Core.DTO.BaseDTO;
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
            var newEquipmentType = await _service.Create(addEquipmentTypeDTO);
            return Ok(newEquipmentType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuccessResponseDTO>> DeleteEquipmentType(string id){
            await _service.Delete(id);
            return Ok(new SuccessResponseDTO("Operacja wykonana prawidłowo"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetEquipmentTypeDTO>> UpdateEquipmentType(AddEquipmentTypeDTO equipmentTypeDTO, string id)
        {
            var updatedData = await _service.Update(equipmentTypeDTO, id);
            return Ok(updatedData);
        }

    }
}