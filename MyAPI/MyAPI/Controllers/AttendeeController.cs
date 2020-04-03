using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeeController : ControllerBase
    {
        public async Task<ActionResult<Attendees>> GetAsync(int id)
        {
            DataAccess _data = new DataAccess();
            
            //get list of attendeeds
            List<Attendees> temp = await _data.GetAttendeesAsync();

            //use LINQ to get specific one
            Attendees a = (from x in temp
                          where x.ID == id
                          select x).FirstOrDefault();

            return a;
        }
    }
}