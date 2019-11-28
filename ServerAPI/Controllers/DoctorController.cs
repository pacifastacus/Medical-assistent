using Microsoft.AspNetCore.Mvc;
using Models;
using MySql.Data.MySqlClient;
using ServerAPI.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("doctor")]
    public class DoctorController : ControllerBase
    {
        MysqlHelper db = new MysqlHelper();
        [HttpGet]
        public ActionResult<IEnumerable<Record>> Get()
        {
            List<Record> records = new List<Record>();
            using (var reader =
                db.SetQuery("select admission.id," +
                                    "patients.insurance_number," +
                                    "patients.first_name," +
                                    "patients.last_name," +
                                    "patients.address," +
                                    "admission.diagnosis," +
                                    "admission.symptomes," +
                                    "admission.last_modified, " +
                                    "admission.registration_date " +
                            "from admission left join patients on patients.id=admission.patient_id " +
                            "order by registration_date asc").ExecuteReader())
            {
                while (reader.Read())
                {
                    var record = new Record()
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("id")),
                        Address = reader.GetString(reader.GetOrdinal("address")),
                        Diagnosis = reader.GetString(reader.GetOrdinal("diagnosis")),
                        InsuranceNumber = reader.GetInt32(reader.GetOrdinal("insurance_number")),
                        Modified = reader.GetDateTime(reader.GetOrdinal("last_modified")),
                        RegistrationDate = reader.GetDateTime(reader.GetOrdinal("registration_date")),
                        Name = reader.GetString(reader.GetOrdinal("first_name")) + " " + reader.GetString(reader.GetOrdinal("last_name")),
                        Symptomes = reader.GetString(reader.GetOrdinal("symptomes"))
                    };
                    records.Add(record);
                }


            }
            return Ok(records);
        }


        [HttpPut]
        public ActionResult Put([FromBody] Record record)
        {
            db.SetQuery("UPDATE admission " +
                        "SET diagnosis=@diagnosis, " +
                            "symptomes=@symptomes, " +
                            "last_modified=@lastModified " +
                        "WHERE id=@id")
                .AddParameter(new MySqlParameter("@lastModified", DateTime.Now))
                .AddParameter(new MySqlParameter("@diagnosis", record.Diagnosis))
                .AddParameter(new MySqlParameter("@symptomes", record.Symptomes))
                .AddParameter(new MySqlParameter("@id", record.ID))
                .ExecuteNonQuery();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) 
        {
            db.SetQuery("delete from admission where id=@id")
                .AddParameter(new MySqlParameter("@id", id))
                .ExecuteNonQuery();
            return Ok();
        }


    }
}
