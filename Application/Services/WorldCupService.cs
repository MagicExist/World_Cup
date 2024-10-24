using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO_s;
using Application.Mappings;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using World_Cup.DataBase;

namespace Application.Services
{
    public class WorldCupService
    {
        private readonly IWorldCup worldCupRepository;
        private readonly WorldCupsDbContext _context;

        public WorldCupService(IWorldCup worldCupRepository, WorldCupsDbContext context)
        {
            this.worldCupRepository = worldCupRepository;
            _context = context;
        }

        public async Task<LinkedList<ChampionShipDTO>> GetChampionShipsAsync() 
        {
            var WorldCupList = await worldCupRepository.GetChampionShipAsync();
            LinkedList<ChampionShipDTO> WorldCupDTOList = new LinkedList<ChampionShipDTO>();
            foreach (var championship in WorldCupList)
            {
                var ChampDTO = new ChampionShipDTO() 
                {
                    name = championship.ChampionShip1,
                    Year = championship.Year
                };
                WorldCupDTOList.AddLast(ChampDTO);
            }

            return WorldCupDTOList;
        }

        public async Task<ChampionshipTeamsDTO> GetTeamsByChampionshipAsync(int championshipId)
        {
            // Obtener información del campeonato
            var championshipInfo = await _context.ChampionShips
                .Where(c => c.Id == championshipId)
                .Select(c => new { c.ChampionShip1, c.Year })
                .FirstOrDefaultAsync();

            if (championshipInfo == null)
            {
                throw new KeyNotFoundException($"No se encontró el campeonato con ID {championshipId}");
            }

            // Obtener equipos
            var teams = await worldCupRepository.GetTeamsByChampionshipAsync(championshipId);

            // Crear y retornar el DTO
            return new ChampionshipTeamsDTO
            {
                ChampionshipName = championshipInfo.ChampionShip1,
                Year = championshipInfo.Year,
                Teams = teams.Select(t => new CountryDTO
                {
                    Id = t.Id,
                    Name = t.Country1,
                    Entity = t.Entity,
                    Flag = t.Flag,
                    EntityLogo = t.EntityLogo
                })
            };
        }
    }
}
