using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace BilgeAdam.Data.Transform.Managers
{
    class DapperDataManager : DataManagerBase, IDataManager
    {
        public IEnumerable<T> Load<T>(string query, params SqlParameter[] parameters)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var parameter in parameters)
            {
                dictionary.Add(parameter.ParameterName, parameter.Value);
            }
            
            var p = new DynamicParameters(dictionary);
            var result = Connection.Query<T>(query, p);
            Connection.Close();
            return result;
        }
    }
}

// TODO: Örnek Dapper
/*
var parameters = new { name = "A" };
var sql = "select * from customers where customerid like @name";
var result = Connection.Query(sql, parameters);
 */
