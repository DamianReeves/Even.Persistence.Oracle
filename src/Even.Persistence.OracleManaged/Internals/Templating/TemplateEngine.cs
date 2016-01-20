using System;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Even.Persistence.OracleManaged.Internals.Templating
{
    public class TemplateEngine:ITemplateEngine
    {
        private static Func<ITemplateEngine> _templateEngineProvider = ()=> new TemplateEngine(); 
        private static readonly Lazy<ITemplateEngine> InstanceAccesor  = new Lazy<ITemplateEngine>(_templateEngineProvider);
        
        
        public string Generate(string template, object data)
        {
            string pattern = @"{{(?<key>[^,:]+?)(,(?<align>-?\d+))?(:(?<format>.+?))?}}";
            Regex regex = new Regex(pattern, RegexOptions.ExplicitCapture);
            return regex.Replace(template, match => GetReplacement(match, data));
        }

        public static ITemplateEngine Default => InstanceAccesor.Value;

        internal static void SetTemplateEngineProvider(Func<ITemplateEngine> templateEngineProvider)
        {
            if (templateEngineProvider == null) throw new ArgumentNullException(nameof(templateEngineProvider));
            _templateEngineProvider = templateEngineProvider;
        }

        private string GetReplacement(Match match, object data)
        {
            string alignment = match.Groups["align"].Value;
            string formatting = match.Groups["format"].Value;
            string format = GetFormat(alignment, formatting);

            string key = match.Groups["key"].Value;
            PropertyInfo property = data.GetType().GetProperty(key);
            object value = property == null ? null : property.GetValue(data);

            string replacement = String.Format(format, value);
            return replacement;
        }

        private string GetFormat(string alignment, string formatting)
        {
            StringBuilder format = new StringBuilder("{0");
            if (!String.IsNullOrWhiteSpace(alignment))
            {
                format.Append(',');
                format.Append(alignment);
            }
            if (!String.IsNullOrWhiteSpace(formatting))
            {
                format.Append(':');
                format.Append(formatting);
            }
            format.Append("}");
            return format.ToString();
        }
    }
}
