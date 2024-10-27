using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public WorldCupService(IWorldCup worldCupRepository, WorldCupsDbContext context)
        {
            this.worldCupRepository = worldCupRepository;
        }

        public async Task<LinkedList<ChampionShipDTO>> GetChampionShipsAsync() 
        {
            var WorldCupList = await worldCupRepository.GetChampionShipAsync();
            LinkedList<ChampionShipDTO> WorldCupDTOList = new LinkedList<ChampionShipDTO>();
            foreach (var championship in WorldCupList)
            {
                var ChampDTO = new ChampionShipDTO() 
                {
                    Id = championship.Id,
                    Name = championship.ChampionShip1,
                    Year = championship.Year
                };
                WorldCupDTOList.AddLast(ChampDTO);
            }

            return WorldCupDTOList;
        }

        public async Task<ChampionshipTeamsDTO> GetTeamsByChampionshipAsync(int championshipId)
        {
            // Obtener información del campeonato usando el repositorio
            var championshipInfo = await worldCupRepository.GetChampionshipInfoAsync(championshipId);

            if (!championshipInfo.HasValue)
            {
                throw new KeyNotFoundException($"No se encontró el campeonato con ID {championshipId}");
            }

            // Obtener equipos
            var teams = await worldCupRepository.GetTeamsByChampionshipAsync(championshipId);

            // Crear y retornar el DTO
            return new ChampionshipTeamsDTO
            {
                ChampionshipName = championshipInfo.Value.Name,
                Year = championshipInfo.Value.Year,
                Teams = teams.Select(t => new CountryDTO
                {
                    Id = t.Id,
                    Name = t.Country1,
                    Entity = t.Entity
                })
            };
        }

        public IEnumerable<dynamic> GetPositionTableByGroup(int group)
        {
            IEnumerable<Match> matchesByGroup = worldCupRepository.GetPositionTableByGroup(group);

            var query = (from match in matchesByGroup
                         let countryIds = new[] { match.FirstCountryId, match.SecondCountryId }
                         from countryId in countryIds
                         group match by countryId into matchByCountry
                         select new
                         {
                             key = matchByCountry.Key,
                             PJ = matchByCountry.Count(),
                             PG = matchByCountry.Count(m =>
                                                    (m.FirstCountryId == matchByCountry.Key && m.FirstCountryGoals > m.SecondCountryGoals) ||
                                                    (m.SecondCountryId == matchByCountry.Key && m.SecondCountryGoals > m.FirstCountryGoals)),
                             PE = matchByCountry.Count(m =>
                                                    (m.FirstCountryId == matchByCountry.Key && m.FirstCountryGoals == m.SecondCountryGoals) ||
                                                    (m.SecondCountryId == matchByCountry.Key && m.SecondCountryGoals == m.FirstCountryGoals)),
                             PP = matchByCountry.Count(m =>
                                                    (m.FirstCountryId == matchByCountry.Key && m.FirstCountryGoals < m.SecondCountryGoals) ||
                                                    (m.SecondCountryId == matchByCountry.Key && m.SecondCountryGoals < m.FirstCountryGoals)),
                             GF = matchByCountry.Select(m => m.FirstCountryId == matchByCountry.Key ? m.FirstCountryGoals : m.SecondCountryGoals).Sum(g => g),
                             GC = matchByCountry.Select(m => m.FirstCountryId == matchByCountry.Key ? m.SecondCountryGoals : m.FirstCountryGoals).Sum(g => g),
                             Points = matchByCountry.Sum(m =>
                                                    m.FirstCountryId == matchByCountry.Key && m.FirstCountryGoals > m.SecondCountryGoals ? 3 :
                                                    m.SecondCountryId == matchByCountry.Key && m.SecondCountryGoals > m.FirstCountryGoals ? 3 :
                                                    m.FirstCountryGoals == m.SecondCountryGoals ? 1 : 0)
                         }).OrderBy(m => m.Points);
            return query;
        }

        public async IAsyncEnumerable<GroupDTO> GetGroupsByChampionShipAsync(int championShipId)
        {
            var groupsList = await worldCupRepository.GetGroupsByChampionShipAsync(championShipId);
            foreach (var group in groupsList) 
            {
                yield return new GroupDTO
                {
                    Id = group.Id,
                    Group = group.Group1,
                };
            }

        }
    }
}
