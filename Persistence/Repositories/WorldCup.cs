using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World_Cup.DataBase;

namespace Persistence.Repositories
{
    public class WorldCup : IWorldCup
    {
        private readonly WorldCupsDbContext _context;

        public WorldCup(WorldCupsDbContext context) 
        {
            _context = context;
        }

        public async Task<ChampionShip[]> GetChampionShipAsync()
        {
            var response = await _context.ChampionShips.ToArrayAsync();
            return response;
        }

        public async Task<bool> ChampionshipExistsAsync(int championshipId)
        {
            return await _context.ChampionShips.AnyAsync(c => c.Id == championshipId);
        }

        public async Task<(string Name, int Year)?> GetChampionshipInfoAsync(int championshipId)
        {
            var championship = await _context.ChampionShips
                .Where(c => c.Id == championshipId)
                .Select(c => new {c.ChampionShip1, c.Year})
                .FirstOrDefaultAsync();

            if (championship == null)
                return null;

            return (championship.ChampionShip1, championship.Year);
        }

        public async Task<IEnumerable<Country>> GetTeamsByChampionshipAsync(int championshipId)
        {
            var championship = await _context.ChampionShips
                .FirstOrDefaultAsync(c => c.Id == championshipId);

            if (championship == null)
            {
                throw new KeyNotFoundException($"No se encontró el campeonato con ID {championshipId}");
            }

            var teams = await _context.Countries
                .Join(
                    _context.CountriesByGroups,
                    country => country.Id,
                    cbg => cbg.CountryId,
                    (country, cbg) => new { country, cbg }
                )
                .Join(
                    _context.Groups.Where(g => g.ChampionShipId == championshipId),
                    x => x.cbg.GroupId,
                    group => group.Id,
                    (x, group) => x.country
                )
                .Distinct()
                .OrderBy(c => c.Country1)
                .ToListAsync();

            return teams ?? Enumerable.Empty<Country>();
        }

        public IEnumerable<Match> GetPositionTableByGroup(int groupId)
        {
            var matchesByGroup = (from groups in _context.Groups
                                  join championship in _context.ChampionShips on groups.ChampionShipId equals championship.Id
                                  join matches in _context.Matches on championship.Id equals matches.ChampionShipId
                                  where groups.Id == groupId
                                  && _context.CountriesByGroups
                                     .Where(cbg => cbg.GroupId == groupId)
                                     .Select(cbg => cbg.CountryId)
                                     .Contains(matches.FirstCountryId)
                                  select matches).ToList();
            return matchesByGroup;
        }
    }

}

