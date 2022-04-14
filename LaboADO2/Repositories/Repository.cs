using System.Data.Common;
using System.Data.SqlClient;

namespace LaboADO.Repositories
{
    public abstract class Repository<T, TKey> : IDisposable
    {
        protected DbConnection _connection;

        public string TableName { get; set; }

        public Repository(string tableName)
        {
            _connection = new SqlConnection(@"Data Source=DESKTOP-DHBA6A4\SQLEXPRESS;Initial Catalog=Labo;Integrated Security=True;MultipleActiveResultSets=true;Connection Timeout=90");
            TableName = tableName;
            _connection.Open();
        }

        protected abstract T ToEntity(DbDataReader reader);

        public IEnumerable<T> FindAll()
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"Select * from {TableName};";

            DbDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                yield return ToEntity(reader);
            }

            reader.Close();
        }

        public T FindOneById(TKey id)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {TableName} WHERE {TableName}_id = @id";
            command.Parameters.Add(new SqlParameter("id", id));
            var reader = command.ExecuteReader();
            reader.Read();
            T entity = ToEntity(reader);
            reader.Close();
            return entity;
        }

        public void Remove(TKey id)
        {            
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"DELETE {TableName} FROM {TableName}" +
                                  $" WHERE {TableName}_id = @id";
            command.Parameters.Add(new SqlParameter("id", id));
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
