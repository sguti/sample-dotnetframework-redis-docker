using RCNETAPI.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RCNETAPI.Controllers
{
    public class ValuesController : ApiController
    {
        readonly IDatabase db = RedisStore.RedisCache;       

        // GET api/values/5
        public async Task<string> Get(string key)
        {
            return await db.StringGetAsync(key);
        }

        // POST api/values
        public async Task<bool> Post(string key,string value)
        {
            return await db.StringSetAsync(key, value, when: When.NotExists);
        }

        // PUT api/values/5
        public async Task<bool> Put([FromBody] string key, [FromBody] string value)
        {
            return await db.StringSetAsync(key, value, when:  When.Exists);
        }

        // DELETE api/values/5
        public async Task<Boolean> Delete(string key)
        {
            return await db.KeyDeleteAsync(key);
        }
    }
}
