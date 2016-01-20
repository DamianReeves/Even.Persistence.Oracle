using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Even.Persistence.OracleManaged.Configuration
{
    public class TableSettings
    {
        private readonly string _eventsTableName;        
        private readonly string _projectionIndexTableName;
        private readonly string _projectionCheckpointTableName;
        private readonly string _schemaName;

        public TableSettings(string eventsTableName, string projectionIndexTableName, string projectionCheckpointTableName, string schemaName)
        {            
            if (projectionCheckpointTableName == null) throw new ArgumentNullException(nameof(projectionCheckpointTableName));
            if (projectionIndexTableName == null) throw new ArgumentNullException(nameof(projectionIndexTableName));
            if (eventsTableName == null) throw new ArgumentNullException(nameof(eventsTableName));

            _eventsTableName = eventsTableName;
            _projectionIndexTableName = projectionIndexTableName;
            _projectionCheckpointTableName = projectionCheckpointTableName;
            _schemaName = schemaName;
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

        public string SchemaName
        {
            get
            {
                return _schemaName;
            }
        }
    }
}
