using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JoinUsAPI.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        List<string> values = new List<String> { "value1", "value2" };
        public IEnumerable<string> Get()
        {
            return values;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return values[id];
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            values.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
