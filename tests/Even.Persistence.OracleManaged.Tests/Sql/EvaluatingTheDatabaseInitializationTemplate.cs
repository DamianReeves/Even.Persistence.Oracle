using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Even.Persistence.OracleManaged.Configuration;
using Even.Persistence.OracleManaged.Internals.Templating;
using TestStack.BDDfy;
using Xunit;

namespace Even.Persistence.OracleManaged.Sql
{
    public class EvaluatingTheDatabaseInitializationTemplate
    {
        private EventStoreDatabaseSchemaSettings _schemaSettings;
        private ITemplateEngine _templateEngine;
        private SqlScriptProvider _scriptProvider;
        private string _templateResult;

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }

        public void GivenASqlScriptProvider()
        {
            _templateEngine = TemplateEngine.Default;
            _scriptProvider = new SqlScriptProvider(_templateEngine);
            _schemaSettings = new EventStoreDatabaseSchemaSettings(
                "UnitTesting",
                false,
                "Events_Test",
                "ProjectionIndex_Test",
                "ProjectionCheckpoint_Test");
        }

        public async void WhenWeCallGetInitializationScriptAsync()
        {
            _templateResult = await _scriptProvider.GetInitializationScriptAsync(_schemaSettings);
        }

        public void ThenItShouldReplaceTheEvenTable()
        {
            Assert.Contains(
                "select object_name from user_objects where (object_name = upper('Events_Test') and object_type = 'TABLE'));",
                _templateResult);
        }

        public void AndItShouldReplaceTheProjectionIndexTable()
        {
            Assert.Contains(
                "select object_name from user_objects where (object_name = upper('ProjectionIndex_Test') and object_type = 'TABLE'));",
                _templateResult);
        }

        public void AndItShouldReplaceTheProjectionCheckpointTable()
        {
            Assert.Contains(
                "select object_name from user_objects where (object_name = upper('ProjectionCheckpoint_Test') and object_type = 'TABLE'));",
                _templateResult);
        }

        public void AndItShouldReplaceTheSchemaName()
        {
            Assert.Contains(
                "execute immediate('alter session set current_schema =UnitTesting');",
                _templateResult);
        }
    }
}
