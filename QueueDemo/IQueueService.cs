using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDemo
{
    public interface IQueueService : IDisposable
    {
        Task EnqueueAsync(string message);
        Task<string> DequeueAsync();
    }
}