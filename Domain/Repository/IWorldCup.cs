using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World_Cup.DataBase;

namespace Domain.Repository
{
    public interface IWorldCup
    {
        Task<ChampionShip[]> GetChampionShipAsync();
        Task<IEnumerable<Country>> GetTeamsByChampionshipAsync(int championshipId);
        Task<bool> ChampionshipExistsAsync(int championshipId);
        IEnumerable<Match> GetPositionTableByGroup(int groupId);
        Task<(string Name, int Year)?> GetChampionshipInfoAsync(int championshipId);
    }
}
