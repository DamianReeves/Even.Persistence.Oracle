using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Even.Persistence.OracleManaged.Configuration;
using Even.Persistence.OracleManaged.Internals.Templating;

namespace Even.Persistence.OracleManaged.Sql
{
    public class SqlScriptProvider: ISqlScriptProvider
    {
        private static readonly Type AnchorType = typeof(SqlScriptProvider);
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

        public async Task<string> GetInitializationScriptAsync(EventStoreDatabaseSchemaSettings schemaSettings)
        {            
            var template = await GetScriptTemplateAsync("CreateTables.template.sql");
            return TemplateEngine.Generate(template, schemaSettings);
        }

        public Task<string> GetScriptTemplateAsync(string templateName)
        {
            using (var stream = AnchorType.Assembly.GetManifestResourceStream(AnchorType, templateName))
            {
                if(stream == null) throw new InvalidOperationException(
                    $"Cannot locate template: {templateName}, be sure that you have specified the correct template name and that you have marked the template file as an \"EmbeddedResource\".");
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEndAsync();                   
                }
            }
        }
    }
}
