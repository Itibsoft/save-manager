using System;
using System.Threading.Tasks;

namespace SticksCandy.Services.Save
{
    public interface ISerializer<TSerializeData>
    {
        public Task<TSerializeData> Serialize<TData>(TData data);
        public Task<TData> Deserialize<TData>(TSerializeData data);
    }
}