using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Infrastructure.CrossCutting.Redis
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public RedisService(string connectionString)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
        }

        public async Task SetValueAsync(string key, string value, TimeSpan? expiration = null)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(key, value, expiration);
        }

        public async Task<string?> GetValueAsync(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.KeyExistsAsync(key);
        }

        public async Task RemoveValueAsync(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.KeyDeleteAsync(key);
        }
    }
}
