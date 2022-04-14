using LaboADO.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace LaboADO.Repositories
{
    public class EmplacementRepository : Repository<Emplacement, long>
    {
        public EmplacementRepository() : base("emplacement")
        {

        }
        public void Add(Emplacement emplacement)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"INSERT INTO {TableName} VALUES (@casier, @etagere, @libre)";
            command.Parameters.Add(new SqlParameter("casier", emplacement.Casier));
            command.Parameters.Add(new SqlParameter("etagere", emplacement.Etagere));
            command.Parameters.Add(new SqlParameter("libre", emplacement.Disponible));
            command.ExecuteNonQuery();
        }

        public void Edit(long emplacementId, Emplacement emplacement)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"UPDATE {TableName} SET casier = @casier, etagere = @etagere, libre = @libre WHERE {TableName}_id = @id";
            command.Parameters.Add(new SqlParameter("id", emplacementId));
            command.Parameters.Add(new SqlParameter("casier", emplacement.Casier));
            command.Parameters.Add(new SqlParameter("etagere", emplacement.Etagere));
            command.Parameters.Add(new SqlParameter("libre", emplacement.Disponible));
            command.ExecuteNonQuery();
        }

        public void ModifierPlace(long id, bool isEmpty)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"UPDATE {TableName} " +
                                    $"SET libre = @isEmpty " +
                                    $"WHERE {TableName}_id = @id";
            command.Parameters.Add(new SqlParameter("id", id));
            command.Parameters.Add(new SqlParameter("isEmpty", isEmpty));
            command.ExecuteNonQuery();
        }

        public void ModifierAllPlaces()
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"UPDATE {TableName} " +
                                    $"SET libre = @true ";
            command.Parameters.Add(new SqlParameter("true", true));
            command.ExecuteNonQuery();
        }

        public IEnumerable<Emplacement> GetWithFilters(string? keyword, int limit)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = @$"
                        SELECT * FROM {TableName} 
                        WHERE casier LIKE @p1 OR etagere LIKE @p1 
                        ORDER BY {TableName}_id 
                        OFFSET 0 ROWS 
                        FETCH NEXT @p3 ROWS ONLY 
            ;";
            command.Parameters.Add(new SqlParameter("p1", keyword + "%"));
            command.Parameters.Add(new SqlParameter("p3", limit));

            DbDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                yield return new Emplacement()
                {
                    EmplacementId = (long)reader["emplacement_id"],
                    Casier = (string)reader["casier"],
                    Etagere = (string)reader["etagere"],
                    Disponible = (bool)reader["libre"]
                };
            }
        }
        protected override Emplacement ToEntity(DbDataReader reader)
        {
            return new Emplacement
            {
                EmplacementId = (long)reader["emplacement_id"],
                Casier = (string)reader["casier"],
                Etagere = (string)reader["etagere"],
                Disponible = (bool)reader["libre"]
            };
        }
    }
}
