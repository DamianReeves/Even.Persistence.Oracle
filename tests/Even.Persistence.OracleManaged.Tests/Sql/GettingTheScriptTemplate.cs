using Even.Persistence.OracleManaged.Internals.Templating;
using FluentAssertions;
using TestStack.BDDfy;
using Xunit;

namespace Even.Persistence.OracleManaged.Sql
{
    public class GettingTheScriptTemplate
    {
        private SqlScriptProvider _sqlScriptProvider;
        private string _templateName;
        private string _templateText;
        private string _expectedContent;

        [Fact]
        public void Execute()
        {
            this.WithExamples(new ExampleTable("TemplateName","ExpectedContent")
            {
                { "InitializeDatabase.template.sql", "-- TemplateName: InitializeDatabase.template.sql"},
                { "WriteEvents.template.sql", "-- TemplateName: WriteEvents.template.sql"}
            }).BDDfy();
        }

        public void Setup()
        {
            _sqlScriptProvider = new SqlScriptProvider(TemplateEngine.Default);
        }

        public void GivenATemplateNameOf__templateName__(string templateName)
        {
            _templateName = templateName;
        }

        public void AndGiven__expectedContent__(string expectedContent)
        {
            _expectedContent = expectedContent;
        } 

        public void WhenCallingGetScriptTemplateAsync()
        {
            _templateText = _sqlScriptProvider.GetScriptTemplateAsync(_templateName).Result;
        }

        public void ThenTheTemplateShouldNotBeNull()
        {
            Assert.NotNull(_templateText);
            _templateText.Should().NotBeNull();
        }

        public void AndThenTheTemplateShouldContainTheExpectedContent()
        {
            _templateText.Should().Contain(_expectedContent);
        }
    }
}