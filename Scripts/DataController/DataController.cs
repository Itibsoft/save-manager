using System.Threading.Tasks;

namespace SticksCandy.Services.Save
{
    public abstract class DataController<TData> where TData : class
    {
        protected TData Data { get; private set; }
        protected abstract string Name { get; }

        private readonly IStorage _storage;

        public DataController(IStorage storage)
        {
            _storage = storage;
        }

        public async Task LoadAsync()
        {
            Data = await _storage.Get<TData>(Name);

            if (Data == default)
            {
                Data = CreateDefaultData();
            }
        }

        public async Task SaveAsync()
        {
            if (Data == default) return;

            await _storage.Put(Name, Data);
        }

        protected abstract TData CreateDefaultData();
    }
}