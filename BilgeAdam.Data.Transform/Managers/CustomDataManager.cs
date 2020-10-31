using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BilgeAdam.Data.Transform.Managers
{
    internal class CustomDataManager : DataManagerBase, IDataManager
    {
        public IEnumerable<T> Load<T>(string query, params SqlParameter[] parameters)
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            using (var cmd = new SqlCommand(query, Connection))
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var instance = Activator.CreateInstance<T>();
                        foreach (var property in properties)
                        {
                            var value = reader[property.Name];
                            property.SetValue(instance, value);
                        }
                        yield return instance;
                    }
                }
            }
            Connection.Close();
        }
    }
}