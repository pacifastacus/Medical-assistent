using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ServerAPI.DBContext;
using ServerAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("hello")]
    public class HelloController: ControllerBase
    {
        
        
            [HttpGet]
            public ActionResult<string> Get()
            {
            

                return Ok("hello");
            }      
            
        
    }
}
