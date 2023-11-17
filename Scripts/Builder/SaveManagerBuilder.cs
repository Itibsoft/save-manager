namespace SticksCandy.Services.Save
{
    public class SaveManagerBuilder
    {
        public static SaveManagerBuilder Create()
        {
            return new SaveManagerBuilder();
        }

        public ISaveManager Build()
        {
            return new SaveManager(default);
        }
    }
}