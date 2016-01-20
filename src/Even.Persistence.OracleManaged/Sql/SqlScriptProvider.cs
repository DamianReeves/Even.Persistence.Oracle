using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Even.Persistence.OracleManaged.Internals.Templating;

namespace Even.Persistence.OracleManaged.Sql
{
    public class SqlScriptProvider
    {
        private static readonly Type MyType = typeof(SqlScriptProvider);
        private readonly ITemplateEngine _templateEngine;

        public SqlScriptProvider(ITemplateEngine templateEngine)
        {
            if (templateEngine == null) throw new ArgumentNullException(nameof(templateEngine));
            _templateEngine = templateEngine;
        }

        public ITemplateEngine TemplateEngine
        {
            get { return _templateEngine; }
        }

        public async Task<string> GetInitializationScriptAsync(string eventsTableName, string projectionIndexTableName, string projectionCheckpointTableName, string schema)
        {

            using (var stream = MyType.Assembly.GetManifestResourceStream(MyType, "InitializeDatabase.template.sql"))
            {
                using (var reader = new StreamReader(stream))
                {
                    var text = await reader.ReadToEndAsync();
                    var templateData = new
                    {
                        EventsTableName = eventsTableName,
                        ProjectionIndexTableName = projectionIndexTableName,
                        ProjectionCheckpointTableName = projectionCheckpointTableName,
                        Schema = schema
                    };
                    return TemplateEngine.Generate(text, templateData);
                }
            }           
        }
    }

}
