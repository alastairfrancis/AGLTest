using System;
using Microsoft.AspNetCore.Mvc;
using AGLTest.Common.Services;
using AGLTest.Common.Models;

namespace AGLTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        #region Constructors

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        #endregion

        #region Public APIs

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _petService.GetAll();
                return result.Success ? (IActionResult)Ok(result.Data) : BadRequest(result);
            }
            catch (Exception ex)
            {
                // return error message as json payload
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search([FromQuery] PetFilterCriteria searchCriteria)
        {
            try
            {
                var result = _petService.Search(searchCriteria);
                return result.Success ? (IActionResult)Ok(result.Data) : BadRequest(result);
            }
            catch (Exception ex)
            {
                // return error message as json payload
                return BadRequest(new { error = ex.Message });
            }
        }

        #endregion
    }
}
