using System.Threading.Tasks;

namespace SticksCandy.Services.Save
{
    public interface IStorage
    {
        public Task<TData> Get<TData>(string key) where TData : class;
        public Task Put<TData>(string key, TData data);
        public Task Delete<TData>(string key);
    }
}