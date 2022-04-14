using LaboADO.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace LaboADO.Repositories
{
    public class FournisseurRepository : Repository<Fournisseur, long>
    {
        public FournisseurRepository() : base("fournisseur")
        {
        }

        public void Add(Fournisseur fournisseur)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"INSERT INTO {TableName} VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7)";
            command.Parameters.Add(new SqlParameter("p1", fournisseur.Nom));
            command.Parameters.Add(new SqlParameter("p2", fournisseur.Prenom));
            command.Parameters.Add(new SqlParameter("p3", fournisseur.NumeroTelephone));
            command.Parameters.Add(new SqlParameter("p4", fournisseur.NumeroFax));
            command.Parameters.Add(new SqlParameter("p5", fournisseur.Email));
            command.Parameters.Add(new SqlParameter("p6", fournisseur.Website));
            command.Parameters.Add(new SqlParameter("p7", fournisseur.AdresseId));

            command.ExecuteNonQuery();
        }

        public void Edit(Fournisseur fournisseur)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"UPDATE {TableName} " +
                                  $"SET nom = @p2, prenom = @p3, telephone = @p4, fax = @p5, website = @p6," +
                                  $" email = @p7, adresse_id = @p8" +
                                  $" WHERE {TableName}_id = @p1";
            command.Parameters.Add(new SqlParameter("p1", fournisseur.FournisseurId));
            command.Parameters.Add(new SqlParameter("p2", fournisseur.Nom));
            command.Parameters.Add(new SqlParameter("p3", fournisseur.Prenom));
            command.Parameters.Add(new SqlParameter("p4", fournisseur.NumeroTelephone));
            command.Parameters.Add(new SqlParameter("p5", fournisseur.NumeroFax));
            command.Parameters.Add(new SqlParameter("p6", fournisseur.Website));
            command.Parameters.Add(new SqlParameter("p7", fournisseur.Email));
            command.Parameters.Add(new SqlParameter("p8", fournisseur.AdresseId));
            command.ExecuteNonQuery();
        }

        public void SetAdresseId(long adresseId, long fournisseurId)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"UPDATE {TableName} SET adresse_id = @adresseId WHERE {TableName}_id = @fournisseurId";
            command.Parameters.Add(new SqlParameter("adresseId", adresseId));
            command.Parameters.Add(new SqlParameter("fournisseurId", fournisseurId));
            command.ExecuteNonQuery();
        }

        public IEnumerable<Fournisseur> GetWithFilters(string? keyword, int codepostal, string? location, int limit)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = @$"SELECT *
                                    FROM {TableName} as F
                                    INNER JOIN adresse as A ON F.{TableName}_id = A.{TableName}_id
                                    WHERE F.nom LIKE @p1 OR F.prenom LIKE @p1 OR F.email LIKE @p1 OR F.website LIKE @p1
                                        AND (@p2 = 0 OR A.codepostal LIKE @p2)
                                        AND (A.rue LIKE @p3 OR A.ville LIKE @p3 OR A.pays LIKE @p3)
                                    ORDER BY F.{TableName}_id
                                    OFFSET 0 ROWS
                                    FETCH NEXT @p4 ROWS ONLY
            ;";
            command.Parameters.Add(new SqlParameter("p1", keyword + "%"));
            command.Parameters.Add(new SqlParameter("p2", codepostal));
            command.Parameters.Add(new SqlParameter("p3", location + "%"));
            command.Parameters.Add(new SqlParameter("p4", limit));

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return
                        new Fournisseur()
                        {
                            FournisseurId = (long)reader["fournisseur_id"],
                            Nom = (string)reader["nom"],
                            Prenom = (string)reader["prenom"],
                            NumeroTelephone = (string)reader["telephone"],
                            NumeroFax = (string)reader["fax"],
                            Email = (string)reader["email"],
                            Website = (string)reader["website"],
                            AdresseId = (long)reader["adresse_id"],
                            Adresse = new Adresse
                            {
                                AdresseId = (long)reader["adresse_id"],
                                Numero = (int)reader["numero"],
                                Rue = (string)reader["rue"],
                                Ville = (string)reader["ville"],
                                Codepostal = (int)reader["codepostal"],
                                Pays = (string)reader["pays"],
                                FournisseurId = (long)reader["fournisseur_id"]
                            }
                        }
                ;                
            }
            reader.Close();
        }

        protected override Fournisseur ToEntity(DbDataReader reader)
        {
            return new Fournisseur()
            {
                FournisseurId = (long)reader["fournisseur_id"],
                Nom = (string)reader["nom"],
                Prenom = (string)reader["prenom"],
                NumeroTelephone = (string)reader["telephone"],
                NumeroFax = (string)reader["fax"],
                Email = (string)reader["email"],
                Website = (string)reader["website"],
                AdresseId = (long)reader["adresse_id"]
            };
        }
    }
}
