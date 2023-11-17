namespace SticksCandy.Services.Save
{
    public class SaveManager : ISaveManager
    {
        private FastPrefs _fastPrefs;
        
        public SaveManager(IStorage storage)
        {
            _fastPrefs = new FastPrefs(storage);
        }
        
        //Create migration system
        //Manage data controllers
        //Manage storages
        //Snapshots
        //PlayerPrefs
    }
}