using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSC.CM.XaSh.Services
{
    public class SampleDataUploader : IDataUploader
    {
        public async Task<int> GetCountQueuedRecordsWAttemptsAsync()
        {
            Debug.WriteLine("WARN: GetCountQueuedRecordsWAttemptsAsync cannot run with sample data.");
            return 0;
        }

        public async Task QueueAsync(Guid recordId, QueueableObjects objName)
        {
            Debug.WriteLine("WARN: QueueAsync cannot run with sample data.");
        }

        public async Task QueueAsync(int recordId, QueueableObjects objName)
        {
            Debug.WriteLine("WARN: QueueAsync cannot run with sample data.");
        }

        public async Task QueueAsync(string recordId, QueueableObjects objName)
        {
            Debug.WriteLine("WARN: QueueAsync cannot run with sample data.");
        }

        public async Task StartQueuedUpdatesAsync(CancellationToken token)
        {
            Debug.WriteLine("WARN: StartQueuedUpdatesAsync cannot run with sample data.");
        }

        public void StartSafeQueuedUpdates()
        {
            Debug.WriteLine("WARN: StartSafeQueuedUpdates cannot run with sample data.");
        }
    }
}