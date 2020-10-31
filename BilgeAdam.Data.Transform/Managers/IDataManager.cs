using System.Collections.Generic;
using System.Data.SqlClient;

namespace BilgeAdam.Data.Transform.Managers
{
    internal interface IDataManager
    {
        IEnumerable<T> Load<T>(string query, params SqlParameter[] parameters);
    }
}