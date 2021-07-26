using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.WebApi.DTOs
{
    public class FootballTeamDTO
    {
        public string Name { get; set; }
        public double Score { get; set; }
        public string Manager { get; set; }
    }
}
