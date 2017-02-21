using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dgmedia.DataAccess.Builders.Group
{
    public class GroupBuilder
    {
        private List<string> _idClauses;
        private string _resultClause;

        public BsonDocument Document
        {
            get
            {
                return GenerateDocument();
            }
        }

        public GroupBuilder()
        {
            _idClauses = new List<string>();
        }


        public void BuildIdClause(string name, string value)
        {
            if (value != null)
                _idClauses.Add(string.Format("{0} : {1}", name, value));
        }

        public void BuildResultClause(string name, string value)
        {
            if (value != null)
                _resultClause = string.Format("{{{0} : {1}}}", name, value);
        }        

        private BsonDocument GenerateDocument()
        {
            if (!_idClauses.Any() || _resultClause == null)
                throw new ArgumentNullException("The BsonDocument should have a result clause and one id clause at least.");

            return BsonDocument.Parse(string.Format("{{ _id: {{{0}}}, total: {1} }}", string.Join(",", _idClauses), _resultClause));
        }

    }
}
