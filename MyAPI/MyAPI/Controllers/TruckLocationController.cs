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
    public class TruckLocationController : ControllerBase
    {
        public async Task<ActionResult<TruckLocation>> GetAsync()
        {
            DataAccess _data = new DataAccess();

            return await _data.GetTruckLocationAsync();
        }

    }
}