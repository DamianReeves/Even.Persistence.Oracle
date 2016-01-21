using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Even.Persistence.OracleManaged.Configuration
{
    /// <summary>
    /// Provides database schema configuration settings
    /// </summary>
    public class EventStoreDatabaseSchemaSettings
    {
        private readonly string _eventsTableName;        
        private readonly string _projectionIndexTableName;
        private readonly string _projectionCheckpointTableName;
        private readonly string _schema;
        private readonly bool _createTables;

        public EventStoreDatabaseSchemaSettings(string schema, bool createTables = false):this(schema, createTables, "Events", "ProjectionIndex","ProjectionCheckpoint")
        {            
        }

        public EventStoreDatabaseSchemaSettings(
            string schema,
            bool createTables,
            string eventsTableName, 
            string projectionIndexTableName, 
            string projectionCheckpointTableName)
        {
            if (String.IsNullOrWhiteSpace(schema))
                throw new ArgumentException("Argument is required and cannot be null, empty, or all whitespace.", nameof(schema));
            if (String.IsNullOrWhiteSpace(eventsTableName))
                throw new ArgumentException("Argument is required and cannot be null, empty, or all whitespace.", nameof(eventsTableName));
            if (String.IsNullOrWhiteSpace(projectionIndexTableName))
                throw new ArgumentException("Argument is required and cannot be null, empty, or all whitespace.", nameof(projectionIndexTableName));
            if (String.IsNullOrWhiteSpace(projectionCheckpointTableName))
                throw new ArgumentException("Argument is required and cannot be null, empty, or all whitespace.", nameof(projectionCheckpointTableName));

            _schema = schema;
            _createTables = createTables;
            _eventsTableName = eventsTableName;
            _projectionIndexTableName = projectionIndexTableName;
            _projectionCheckpointTableName = projectionCheckpointTableName;            
        }

        public string EventsTableName
        {
            get
            {
                return _eventsTableName;
            }
        }

        public string ProjectionIndexTableName
        {
            get
            {
                return _projectionIndexTableName;
            }
        }

        public string ProjectionCheckpointTableName
        {
            get
            {
                return _projectionCheckpointTableName;
            }
        }

        public string Schema
        {
            get
            {
                return _schema;
            }
        }

        public bool CreateTables
        {
            get { return _createTables; }
        }
    }
}
