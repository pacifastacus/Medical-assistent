using Microsoft.AspNetCore.Mvc;
using Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("doctor")]
    class DoctorController:ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> Get()
        {
            
            return Ok();
        }


    }
}
