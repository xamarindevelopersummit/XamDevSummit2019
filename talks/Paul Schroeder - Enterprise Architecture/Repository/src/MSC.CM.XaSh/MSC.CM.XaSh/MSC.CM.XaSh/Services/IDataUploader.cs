using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSC.CM.XaSh.Services
{
    public interface IDataUploader
    {
        Task<int> GetCountQueuedRecordsWAttemptsAsync();

        Task QueueAsync(Guid recordId, QueueableObjects objName);

        Task QueueAsync(string recordId, QueueableObjects objName);

        Task QueueAsync(int recordId, QueueableObjects objName);

        Task StartQueuedUpdatesAsync(CancellationToken token);

        void StartSafeQueuedUpdates();
    }
}