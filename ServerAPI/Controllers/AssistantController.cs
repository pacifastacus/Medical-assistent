using Microsoft.AspNetCore.Mvc;
using Models;
using MySql.Data.MySqlClient;
using ServerAPI.DBContext;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("assistant")]
    public class AssistantController : ControllerBase
    {
      
       
           
            private MysqlHelper db = new MysqlHelper();



        [HttpGet]
        public ActionResult<IEnumerable<Patient>> Get()
        {
            List<Patient> patients = new List<Patient>();
            using (MySqlDataReader reader = db.SetQuery("select * from patients").ExecuteReader())
            {
                while (reader.Read())
                {
                    var patient = new Patient()
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("id")),
                        FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                        LastName = reader.GetString(reader.GetOrdinal("last_name")),
                        Address = reader.GetString(reader.GetOrdinal("address")),
                        dateOfBirth = reader.GetDateTime(reader.GetOrdinal("date_of_birth")),
                        InsuranceNumber = reader.GetInt32(reader.GetOrdinal("insurance_number"))
                    };
                    patients.Add(patient);
                }
            }

            return Ok(patients);
        }



        [HttpGet("{id:int}")]
        public ActionResult<Patient> Get(int id)
        {
            Console.WriteLine(id);
            using (MySqlDataReader reader = db.SetQuery("select * from patients where id=@id").AddParameter(new MySqlParameter("@id",id)).ExecuteReader())
            {
                if (reader.HasRows)
                {

                    reader.Read();

                    var patient = new Patient()
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("id")),
                        FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                        LastName = reader.GetString(reader.GetOrdinal("last_name")),
                        Address = reader.GetString(reader.GetOrdinal("address")),
                        dateOfBirth = reader.GetDateTime(reader.GetOrdinal("date_of_birth")),
                        InsuranceNumber = reader.GetInt32(reader.GetOrdinal("insurance_number"))
                    };
                    return Ok(patient);
                }
                else 
                {
                    return NotFound();
                }
                
            }            
        }

        [HttpPost]
        public ActionResult Post([FromForm] Patient patient, [FromForm] string symptomes,[FromForm] DateTime lastModified)
        {

            db.SetQuery("INSERT INTO `patients` ( first_name,last_name, insurance_number ,date_of_birth,address) VALUES(@firstName,@lastName,@insuranceNumber,@dateOfBirth,@address);")
                .AddParameter(new MySqlParameter("@firstName", patient.FirstName))
                .AddParameter(new MySqlParameter("@lastName", patient.LastName))
                .AddParameter(new MySqlParameter("@insuranceNumber", patient.InsuranceNumber))
                .AddParameter(new MySqlParameter("@dateOfBirth",patient.dateOfBirth))
                .AddParameter(new MySqlParameter("@address", patient.Address))
                .ExecuteNonQuery();
            int id;
            using (MySqlDataReader reader= db.SetQuery("select id from patients where insurance_number=@insuranceNumber")
                .AddParameter(new MySqlParameter("@insuranceNumber", patient.InsuranceNumber))
                .ExecuteReader())
            {
                 reader.Read();
                 id = reader.GetInt32(reader.GetOrdinal("id"));
            }
            db.SetQuery("insert into admission (patient_id,symptomes,last_modified) values (@patientID,@symptomes,@lastModified)")
                .AddParameter (new MySqlParameter("@patientID",id))
                .AddParameter(new MySqlParameter("@symptomes",symptomes))
                .AddParameter(new MySqlParameter("@lastModified",lastModified))
                .ExecuteNonQuery();
            return Ok();
        }

    }
}
