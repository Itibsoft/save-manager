using System.Threading.Tasks;

namespace SticksCandy.Services.Save.Utils
{
    public static class TaskExtensions
    {
        public static void WaitTask(this Task task)
        {
            task.GetAwaiter().GetResult();
        }
    }
}