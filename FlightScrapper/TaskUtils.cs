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
                    throw finished.Exception.InnerException;
                }
                tasks = tasks.Where(task => !task.IsCompleted).ToArray();
            }

            await Task.WhenAll(tasks);
        }
    }
}
