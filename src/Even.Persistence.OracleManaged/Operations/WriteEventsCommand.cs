using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace Even.Persistence.OracleManaged.Operations
{
    public class WriteEventsOperation
    {
        private readonly Func<OracleConnection> _connectionProvider;

        public WriteEventsOperation(Func<OracleConnection> connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public Func<OracleConnection> ConnectionProvider
        {
            get { return _connectionProvider; }
        }

        public Task Execute(IReadOnlyCollection<IUnpersistedRawEvent> events)
        {
            throw new NotImplementedException();
        }
    }
}
