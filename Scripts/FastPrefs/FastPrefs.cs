using SticksCandy.Services.Save.Utils;

namespace SticksCandy.Services.Save
{
    public class FastPrefs : DataController<PrefsData>
    {
        protected override string Name => "fast_prefs";
        
        private static FastPrefs _instance;

        public FastPrefs(IStorage storage) : base(storage)
        {
            _instance = this;
            LoadAsync().WaitTask();
        }
        
        protected override PrefsData CreateDefaultData()
        {
            return new PrefsData();
        }

        public static void Save()
        {
            _instance.SaveAsync().WaitTask();
        }

        public static float GetFloat(string key, float defaultValue = 0.0f)
        {
            if (_instance.Data.Floats.TryGetValue(key, out var value))
            {
                return value;
            }

            return defaultValue;
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            if (_instance.Data.Ints.TryGetValue(key, out var value))
            {
                return value;
            }

            return defaultValue;
        }

        public static string GetString(string key, string defaultValue = "")
        {
            if (_instance.Data.Strings.TryGetValue(key, out var value))
            {
                return value;
            }

            return defaultValue;
        }

        public static void SetFloat(string key, float value)
        {
            _instance.Data.Floats[key] = value;
            _instance.SaveAsync().WaitTask();
        }

        public static void SetInt(string key, int value)
        {
            _instance.Data.Ints[key] = value;
            _instance.SaveAsync().WaitTask();
        }

        public static void SetString(string key, string value)
        {
            _instance.Data.Strings[key] = value;
            _instance.SaveAsync().WaitTask();
        }

        public static bool HasKeyInt(string key)
        {
            return _instance.Data.Ints.ContainsKey(key);
        }
        
        public static bool HasKeyFloat(string key)
        {
            return _instance.Data.Floats.ContainsKey(key);
        }
        
        public static bool HasKeyString(string key)
        {
            return _instance.Data.Strings.ContainsKey(key);
        }
        
        public static void DeleteAll()
        {
            _instance.Data.Ints.Clear();
            _instance.Data.Floats.Clear();
            _instance.Data.Strings.Clear();
            
            _instance.SaveAsync().WaitTask();
        }

        public static void DeleteKeyInt(string key)
        {
            _instance.Data.Ints.Remove(key);
            _instance.SaveAsync().WaitTask();
        }
        
        public static void DeleteKeyFloat(string key)
        {
            _instance.Data.Floats.Remove(key);
            _instance.SaveAsync().WaitTask();
        }
        
        public static void DeleteKeyString(string key)
        {
            _instance.Data.Strings.Remove(key);
            _instance.SaveAsync().WaitTask();
        }
    }
}