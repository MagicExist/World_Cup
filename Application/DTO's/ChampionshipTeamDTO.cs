using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO_s
{
    public class ChampionshipTeamsDTO
    {
        public string ChampionshipName { get; set; }
        public int Year { get; set; }
        public IEnumerable<CountryDTO> Teams { get; set; }
    }
}
