using LaboADO.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace LaboADO.Repositories
{
    public class AdresseRepository : Repository<Adresse, long>
    {
        public AdresseRepository() : base("adresse")
        {
        }

        public void Add(Adresse adresse, long fournisseurId)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"INSERT INTO {TableName} VALUES (@p1, @p2, @p3, @p4, @p5, @p6)";
            command.Parameters.Add(new SqlParameter("p1", adresse.Numero));
            command.Parameters.Add(new SqlParameter("p2", adresse.Rue));
            command.Parameters.Add(new SqlParameter("p3", adresse.Ville));
            command.Parameters.Add(new SqlParameter("p4", adresse.Codepostal));
            command.Parameters.Add(new SqlParameter("p5", adresse.Pays));
            command.Parameters.Add(new SqlParameter("p6", fournisseurId));

            command.ExecuteNonQuery();
        }    

        public IEnumerable<Adresse> GetWithFilters(string? keyword, int codepostal, int limit)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = @$"
                        SELECT * FROM {TableName}
                        WHERE rue LIKE @p1 OR ville LIKE @p1 OR pays LIKE @p1
                            AND (@p2 = 0 OR codepostal LIKE @p2)
                        ORDER BY {TableName}_id
                        OFFSET 0 ROWS
                        FETCH NEXT @p3 ROWS ONLY
            ;";
            command.Parameters.Add(new SqlParameter("p1", keyword+"%"));
            command.Parameters.Add(new SqlParameter("p2", codepostal));
            command.Parameters.Add(new SqlParameter("p3", limit));

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return new Adresse()
                {
                    AdresseId = (long)reader["adresse_id"],
                    Numero = (int)reader["numero"],
                    Rue = (string)reader["rue"],
                    Ville = (string)reader["ville"],
                    Codepostal = (int)reader["codepostal"],
                    Pays = (string)reader["pays"],
                    FournisseurId = (long)reader["fournisseur_id"]
                };
            }
        }

        public long FindAdresseIdByFournisseurId(long id)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"SELECT adresse_id FROM {TableName} WHERE fournisseur_id = @id";
            command.Parameters.Add(new SqlParameter("id", id));
            DbDataReader reader = command.ExecuteReader();
            reader.Read();
            long adresseId = (long)reader["adresse_id"];
            reader.Close();
            return adresseId;
        }

        public void Edit(Adresse adresse)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"UPDATE {TableName} " +
                                  $"SET numero = @p2, rue = @p3, ville = @p4, codepostal = @p5 , pays = @p6" +
                                  $" WHERE {TableName}_id = @fournisseurId";
            command.Parameters.Add(new SqlParameter("fournisseurId", adresse.FournisseurId));
            command.Parameters.Add(new SqlParameter("p2", adresse.Numero));
            command.Parameters.Add(new SqlParameter("p3", adresse.Rue));
            command.Parameters.Add(new SqlParameter("p4", adresse.Ville));
            command.Parameters.Add(new SqlParameter("p5", adresse.Codepostal));
            command.Parameters.Add(new SqlParameter("p6", adresse.Pays));
            
            command.ExecuteNonQuery();
        }

        protected override Adresse ToEntity(DbDataReader reader)
        {
            return new Adresse()
            {
                AdresseId = (long)reader["adresse_id"],
                Numero = (int)reader["numero"],
                Rue = (string)reader["rue"],
                Ville = (string)reader["ville"],
                Codepostal = (int)reader["codepostal"],
                Pays = (string)reader["pays"],
                FournisseurId = (long)reader["fournisseur_id"]
            };
        }
    }
}
