using ApiRest.Application;
using ApiRest.Entities;
using ApiRest.WebApi.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiRest.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FootballTeamController : ControllerBase
    {
        IApplication<FootballTeam> _football;  //Inyectamos la dependencia

        //constructor
        public FootballTeamController(IApplication<FootballTeam> football)
        {
            _football = football; //Inyectamos la dependencia
        }

        //método que devuelve lista de objetos
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_football.GetAll());
        }

        [HttpPost]
        public IActionResult Save(FootballTeamDTO dto)
        {
            var f = new FootballTeam()
            {
                Name = dto.Name,
                Score = dto.Score,
                Manager = dto.Manager
            };
            return Ok(_football.Save(f));
        }

    }//Fin del Controller

}//Fin del namespace
