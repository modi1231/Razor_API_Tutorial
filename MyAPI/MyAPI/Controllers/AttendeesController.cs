using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MyAPI.Data;
using MyAPI.Models;


namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {
        public async Task<ActionResult<List<Attendees>>> GetAsync()
        {
            StringValues headerValues;
            //Request holds all the header data and you can put a breakpoint here and look through them
            IHeaderDictionary h = Request.Headers;
            // I am concerned with the 'api_key' value
            h.TryGetValue("api_key", out headerValues);

            //if it is not the super secret '1234' then return from the API.
            if (headerValues[0] != "1234")
                return Unauthorized();

            DataAccess _data = new DataAccess();
            return await _data.GetAttendeesAsync();
        }

        //public async Task<ActionResult<List<Attendees>>> GetAsync(int a)
        //{
        //    DataAccess _data = new DataAccess();
        //    return await _data.GetAttendeesAsync();

        //}
    }
}