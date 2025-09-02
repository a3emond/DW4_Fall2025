using System.Data.SqlClient;

namespace SimpleWebApp.Repositories
{
    public abstract class RepositoryBase //TODO:  maybe transform to generic class RepositoryBase<T> where T : class in order to implement common CRUD operations
    {
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["main_db"].ConnectionString
            );
        }
    }
}