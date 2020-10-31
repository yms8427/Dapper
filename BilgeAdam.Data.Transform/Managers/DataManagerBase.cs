using System.Configuration;
using System.Data.SqlClient;

namespace BilgeAdam.Data.Transform.Managers
{
    abstract class DataManagerBase
    {
        private readonly SqlConnection connection;
        public DataManagerBase()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CnnStr"].ConnectionString);
        }
        protected SqlConnection Connection 
        { 
            get 
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
                return connection; 
            } 
        }
    }
}
