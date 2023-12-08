using Microsoft.AspNetCore.Mvc;
using Vagtplan.Data;
using Vagtplan.Interfaces.Services;
using Vagtplan.Models;
using Vagtplan.Models.Dto;
using Vagtplan.Services;

namespace Vagtplan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpPost]
        public async Task<ActionResult<Shift>> PostShift(CreateShiftDto shift)
        {

            return Ok(_shiftService.CreateShift(shift));
        }


    }
}
