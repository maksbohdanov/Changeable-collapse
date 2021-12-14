using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{   [EnableCors()]    
    [Route("api/[controller]")]
    [ApiController]    
    public class CollapseController : ControllerBase
    {
        private readonly DbClient _dbClient = new DbClient();

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dbClient.GetAll();

            if (result == null)
                return NotFound("There are no records in db");            

            return Ok(result);
        }

        //POST
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToDB([FromBody] Collapse collapse)
        {

            var result = await _dbClient.PostCollapse(collapse);            
            if (result == false)
            {
                return BadRequest("Cannot insert value to database. Please see console log");
            }

            return Ok("Value has been successfully added to DB");
        }

        //DELETE
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCollapse([FromQuery] int id)
        {
            var result = await _dbClient.Delete(id);
            if (result == false)
            {
                return BadRequest("Cannot delete value from database. Please see console log");
            }
            return Ok("Value has been successfully deleted from DB");
        }
    }
}
