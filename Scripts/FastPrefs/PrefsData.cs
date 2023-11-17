using System.Collections.Generic;

namespace SticksCandy.Services.Save
{
    public class PrefsData
    {
        public Dictionary<string, string> Strings = new();
        public Dictionary<string, int> Ints = new();
        public Dictionary<string, float> Floats = new();
    }
}