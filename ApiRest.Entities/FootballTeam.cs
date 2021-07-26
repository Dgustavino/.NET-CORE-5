using System;

namespace ApiRest.Entities
{
    public class FootballTeam: Entity
    {
        public string Name { get; set; }
        public double Score { get; set; }
        public string Manager { get; set; }

    }
}
