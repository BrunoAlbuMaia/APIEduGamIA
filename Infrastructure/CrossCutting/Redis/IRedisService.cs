using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Redis
{
    public interface IRedisService
    {
        Task SetValueAsync(string key, string value, TimeSpan? expiration = null);
        Task<string?> GetValueAsync(string key);
        Task<bool> KeyExistsAsync(string key);
        Task RemoveValueAsync(string key);
    }
}
