﻿using Domain.Repository;
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
    }

}

