namespace Even.Persistence.OracleManaged.Internals.Templating
{
    public interface ITemplateEngine
    {
        string Generate(string template, object data);
    }
}