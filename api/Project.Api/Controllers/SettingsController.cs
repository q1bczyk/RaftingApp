using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTO.SettingsDTO;
using Project.Core.Interfaces.IServices.IBusinessServices;

namespace Project.Api.Controllers
{
    public class SettingsController : BaseApiController
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        public async Task<ActionResult<GetSettingsDTO>> GetSettings()
        {
            var settings = await _settingsService.GetSettings();
            return Ok(settings);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")] 
        public async Task<ActionResult<GetSettingsDTO>> UpdateSettings(string id, BaseSettingsDTO newSettings)
        {
            var settings = await _settingsService.Update(newSettings, id);
            return Ok(settings);
        }
    }
}