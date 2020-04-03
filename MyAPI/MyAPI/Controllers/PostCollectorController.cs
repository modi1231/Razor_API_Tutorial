using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCollectorController : ControllerBase
    {
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<int>> PostAsync(object val)
        {
            try
            {
                //convert in bound object to a Dictionary<string,string>
                Dictionary<string, string> temp = JsonConvert.DeserializeObject<Dictionary<string, string>>(val.ToString());

                //print it out to show it worked
                System.Diagnostics.Debug.WriteLine($"{temp["id"]}");
                //   System.Diagnostics.Debug.WriteLine($"{temp["i2d"]}");
            }
            catch (Exception ex)
            {
                //if there is a problem return 'bad request'
                System.Diagnostics.Debug.WriteLine($"{ex.Message}");
                return BadRequest();
            }
            //return ok when it worked
            return Ok();
        }
    }
}