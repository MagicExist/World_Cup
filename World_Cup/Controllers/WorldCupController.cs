using Application.DTO_s;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using World_Cup.DataBase;

namespace World_Cup.Controllers
{
    [ApiController]
    [Route("Api/")]
    public class WorldCupController:ControllerBase
    {
        private readonly WorldCupService _worldCupService;

        public WorldCupController(WorldCupService worldCupService) 
        {
            _worldCupService = worldCupService;
        }

        [HttpGet("GetChampionship")]
        public async Task<IActionResult> GetChampionshipAync() 
        {
            LinkedList<ChampionShipDTO> championShipsList = await _worldCupService.GetChampionShipsAsync();
            return Ok(championShipsList);
        }

        [HttpGet("{championshipId}")]
        public async Task<ActionResult<ChampionshipTeamsDTO>> GetTeamsByChampionship(int championshipId)
        {
            try
            {
                var result = await _worldCupService.GetTeamsByChampionshipAsync(championshipId);

                if (!result.Teams.Any())
                {
                    return Ok(new
                    {
                        result.ChampionshipName,
                        result.Year,
                        Teams = new List<CountryDTO>(),
                        Message = "Este campeonato no tiene equipos registrados"
                    });
                }

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpGet("GetPositionTable")]
        public ActionResult GetPositionTableByGroup([FromQuery]int group)
        {
            return Ok(_worldCupService.GetPositionTableByGroup(group));
        }
    }
}
