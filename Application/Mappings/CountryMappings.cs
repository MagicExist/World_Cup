using Application.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World_Cup.DataBase;

namespace Application.Mappings
{
    public static class CountryMappings
    {
        public static CountryDTO ToDTO(this Country country)
        {
            return new CountryDTO
            {
                Id = country.Id,
                Name = country.Country1,
                Entity = country.Entity,
                Flag = country.Flag,
                EntityLogo = country.EntityLogo
            };
        }
    }
}
