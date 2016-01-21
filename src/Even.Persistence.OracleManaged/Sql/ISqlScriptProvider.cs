using System.Threading.Tasks;
using Even.Persistence.OracleManaged.Configuration;

namespace Even.Persistence.OracleManaged.Sql
{
    public interface ISqlScriptProvider
    {
        Task<string> GetScriptTemplateAsync(string templateName);
        Task<string> GetInitializationScriptAsync(EventStoreDatabaseSchemaSettings schemaSettings);
    }
}