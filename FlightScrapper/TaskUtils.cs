using System.Runtime.ExceptionServices;

namespace FlightScrapper.App
{
    internal static class TaskUtils
    {
        public static async Task ExecuteTasksUntilFirstException<T>(params Task<T>[] tasks)
        {
            while (tasks.Length > 0)
            {
                Task finished = await Task.WhenAny(tasks);

                if (finished.IsFaulted)
                {
                    ExceptionDispatchInfo.Capture(finished.Exception.InnerException).Throw();
                }
                tasks = tasks.Where(task => !task.IsCompleted).ToArray();
            }

            await Task.WhenAll(tasks);
        }
    }
}
