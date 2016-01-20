using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Even.Persistence.OracleManaged.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace Even.Persistence.OracleManaged.Operations
{
    public class WriteEventsOperation: DataAccessOperationBase
    {
        private readonly TableSettings _tableSettings;

        public WriteEventsOperation(TableSettings tableSettings, Func<OracleConnection> connectionProvider):base(connectionProvider)
        {
            _tableSettings = tableSettings;
        }

        public TableSettings TableSettings
        {
            get
            {
                return _tableSettings;
            }
        }

        public virtual string WriteCommandSql => @"
        declare
            type event_id_t is table of number index by binary_integer;
            type streamHash_t is table of number index by binary integer;
            type 
        begin
            
        end;
";

        public Task Execute(IReadOnlyCollection<IUnpersistedRawEvent> events)
        {
            var command = new OracleCommand();
            throw new NotImplementedException();
        }

        
    }
}