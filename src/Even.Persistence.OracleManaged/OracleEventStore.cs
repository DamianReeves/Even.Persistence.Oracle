using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Even.Persistence.OracleManaged.Configuration;
using Even.Persistence.OracleManaged.Internals.Templating;
using Even.Persistence.OracleManaged.Sql;
using Oracle.ManagedDataAccess.Client;

namespace Even.Persistence.OracleManaged
{
    public class OracleEventStore: IEventStore
    {
        private readonly Func<OracleConnection> _connectionFactory;
        private readonly EventStoreDatabaseSchemaSettings _schemaSettings;
        private readonly ISqlScriptProvider _scriptProvider;

        public OracleEventStore(Func<OracleConnection>  connectionFactory, EventStoreDatabaseSchemaSettings schemaSettings, ISqlScriptProvider scriptProvider)
        {
            if (connectionFactory == null) throw new ArgumentNullException(nameof(connectionFactory));
            if (schemaSettings == null) throw new ArgumentNullException(nameof(schemaSettings));
            if (scriptProvider == null) throw new ArgumentNullException(nameof(scriptProvider));
            _connectionFactory = connectionFactory;
            _schemaSettings = schemaSettings;
            _scriptProvider = scriptProvider;
        }

        internal Func<OracleConnection> ConnectionFactory
        {
            get { return _connectionFactory; }
        }

        public ISqlScriptProvider ScriptProvider
        {
            get { return _scriptProvider; }
        }

        public EventStoreDatabaseSchemaSettings SchemaSettings
        {
            get { return _schemaSettings; }
        }

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

        protected virtual async Task BuildCommandsAsync()
        {
            await BuildCommandsAsync();

            if (SchemaSettings.CreateTables)
            {
                
            }
        }

        protected virtual OracleConnection CreateConnection()
        {
            return ConnectionFactory.Invoke();
        }

        protected virtual OracleCommand CreateWriteCommand(Stream stream, IUnpersistedRawEvent e)
        {
            //// order: EventID, StreamHash, StreamName, EventType, UtcTimestamp, Metadata, Payload, PayloadFormat
            //// index: 0        1           2           3          4             5         6        7
            //var command = DB.CreateCommand(CommandText_Write, e.EventID, stream.Hash, stream.Name, e.EventType, e.UtcTimestamp, e.Metadata, e.Payload, e.PayloadFormat);

            //// dbtype may not be inferred because payload may be null - all other fields are "not null"
            //// this avoids issues when the provider generates or converts parameters
            //command.Parameters[5].DbType = DbType.Binary;

            //return command;
            throw new NotImplementedException();
        }

        protected virtual async Task<OracleCommand> CreateWriteEventsCommand(IReadOnlyCollection<IUnpersistedRawStreamEvent> events)
        {
            var commandText = await ScriptProvider.GetInitializationScriptAsync(SchemaSettings);
            var command = new OracleCommand(commandText, CreateConnection());
            // TODO: Add parameters
            return command;
        } 
    }
}
