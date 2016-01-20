using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Even.Persistence.OracleManaged.Operations
{
    public abstract class DataAccessOperationBase
    {
        private readonly Func<OracleConnection> _connectionProvider;
        protected DataAccessOperationBase(Func<OracleConnection> connectionProvider)
        {
            this._connectionProvider = connectionProvider;
            if (connectionProvider == null)
                throw new ArgumentNullException(nameof(connectionProvider));
        }

        public Func<OracleConnection> ConnectionProvider
        {
            get
            {
                return _connectionProvider;
            }
        }

        protected virtual OracleConnection GetConnection()
        {
            return ConnectionProvider.Invoke();
        }

        protected virtual OracleParameter CreateParameter(string parameterName, object value, OracleDbType? dbType = null, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            var parameter = new OracleParameter(parameterName, value);
            if (dbType.HasValue)
            {
                parameter.OracleDbType = dbType.Value;
            }
            parameter.Direction = parameterDirection;

            return parameter;
        }
    }    
}