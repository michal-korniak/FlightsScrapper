using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                var index = Array.IndexOf(tasks, finished);
                tasks = tasks.Where((_, i) => i != index).ToArray();
            }

            await Task.WhenAll(tasks);
        }
    }
}
