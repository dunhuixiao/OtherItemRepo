using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    [Route("api/Values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// get
        /// </summary>
        /// <param name="id">id</param>
        /// <response code="200">状态200</response>
        /// <response code="201">状态201</response>
        /// <response code="400">状态400</response>  
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GETDTO), 200)]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        /// <summary>
        /// GetDTO
        /// </summary>
        public class GETDTO
        {
            /// <summary>
            /// Id
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get;set; }
        }
    }
}
