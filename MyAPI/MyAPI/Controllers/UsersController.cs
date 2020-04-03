using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MyAPI.Data;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //Basic return of data
        public async Task<ActionResult<int>> GetAsync()
        {
            DataAccess _data = new DataAccess();
            return await _data.GetUserCountAsync();
        }
    }
}