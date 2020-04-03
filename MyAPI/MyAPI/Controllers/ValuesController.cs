using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyAPI.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public string Get()
        {
            return "default get";
        }

        [Route("getone")]
        public string GetOne()
        {
            return "1";
        }

        [Route("gettwo")]
        public string GetTwo()
        {
            return "2";
        }

        [Route("getthree")]
        public string GetThree()
        {
            return "3";
        }
    }
}