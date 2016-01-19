using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace Even.Persistence.OracleManaged
{
    public class OracleEventStore: IEventStore
    {
        private readonly Func<OracleConnection> _connectionFactory;

        public OracleEventStore(Func<OracleConnection>  connectionFactory)
        {
            if (connectionFactory == null) throw new ArgumentNullException(nameof(connectionFactory));
            _connectionFactory = connectionFactory;
        }

        internal Func<OracleConnection> ConnectionFactory
        {
            get { return _connectionFactory; }
        }

        /// <summary>
        /// Name of the 'Events' table.
        /// </summary>
        public virtual string EventsTable => "Event";

        /// <summary>
        /// Name of the 'ProjectionIndex' table.
        /// </summary>
        public virtual string ProjectionIndexTable => "ProjectionIndex";

        /// <summary>
        /// Name of the 'ProjectionCheckpoint' table.
        /// </summary>
        public virtual string ProjectionCheckpointTable => "ProjectionCheckpoint";

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public Task WriteAsync(IReadOnlyCollection<IUnpersistedRawStreamEvent> events)
        {
            throw new NotImplementedException();
        }

        public Task WriteStreamAsync(Stream stream, int expectedSequence, IReadOnlyCollection<IUnpersistedRawEvent> events)
        {
            throw new NotImplementedException();
        }

        public Task<long> ReadHighestGlobalSequenceAsync()
        {
            throw new NotImplementedException();
        }

        public Task ReadAsync(long initialSequence, int count, Action<IPersistedRawEvent> readCallback, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task ReadStreamAsync(Stream stream, int initialSequence, int count, Action<IPersistedRawEvent> readCallback, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task ClearProjectionIndexAsync(Stream stream)
        {
            throw new NotImplementedException();
        }

        public Task WriteProjectionIndexAsync(Stream stream, int expectedSequence, IReadOnlyCollection<long> globalSequences)
        {
            throw new NotImplementedException();
        }

        public Task WriteProjectionCheckpointAsync(Stream stream, long globalSequence)
        {
            throw new NotImplementedException();
        }

        public Task<long> ReadProjectionCheckpointAsync(Stream stream)
        {
            throw new NotImplementedException();
        }

        public Task<long> ReadHighestIndexedProjectionGlobalSequenceAsync(Stream stream)
        {
            throw new NotImplementedException();
        }

        public Task<int> ReadHighestIndexedProjectionStreamSequenceAsync(Stream stream)
        {
            throw new NotImplementedException();
        }

        public Task ReadIndexedProjectionStreamAsync(Stream stream, int initialSequence, int count, Action<IPersistedRawEvent> readCallback,
            CancellationToken ct)
        {
            throw new NotImplementedException();
        }       
    }
}
