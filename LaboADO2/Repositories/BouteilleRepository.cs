using LaboADO.Models;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;

namespace LaboADO.Repositories
{
    public class BouteilleRepository : Repository<Bouteille, long>
    {
        public BouteilleRepository() : base("bouteille")
        {
        }

        public IEnumerable<Bouteille> GetWithFilters(
                string? keyword, string? location, int minYear, int maxYear, decimal minAlcool,
                decimal maxAlcool, decimal minVolume, decimal maxVolume, IEnumerable<WineType>? types, byte enstock, int limit)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {TableName} as B " +
                                  $"INNER JOIN fournisseur as F ON F.fournisseur_id = B.fournisseur_id " +
                                  $"INNER JOIN emplacement as E ON E.emplacement_id = B.emplacement_id " +
                                  $"WHERE (B.label LIKE @p1 OR B.marque LIKE @p1 OR B.origine LIKE @p1 OR B.pays LIKE @p1 OR F.nom LIKE @p1 OR F.prenom LIKE @p1) " +
                                  $"AND (E.casier LIKE @location OR E.etagere LIKE @location) " +
                                  $"AND (@minYear is null OR B.date > @minYear) " +
                                  $"AND (@maxYear is null OR B.date < @maxYear) " +
                                  $"AND (@minAlcool = 0 OR B.degree > @minAlcool) " +
                                  $"AND (@maxAlcool >= 0 OR B.degree < @maxAlcool) " +
                                  $"AND (@minVolume = 0 OR B.volume > @minVolume) " +
                                  $"AND (@maxVolume >= 0 OR B.volume < @maxVolume) " +
                                  $"AND (@type = 0 __WHERE_IN__) " +
                                  $"ORDER BY {TableName}_id " +
                                  $"OFFSET 0 ROWS " +
                                  $"FETCH NEXT @limit ROWS ONLY;"
                                  ;      

            DateTime beforeDate = new DateTime();
            DateTime afterDate = new DateTime();
            if (minYear > 1753 || maxYear > 1753)
            {
                string min_year = $"{minYear}0101";
                beforeDate = DateTime.ParseExact(min_year,
                                                    "yyyyMMdd",
                                                    CultureInfo.InvariantCulture,
                                                    DateTimeStyles.None);
                afterDate = DateTime.ParseExact($"{maxYear}0101",
                                                    "yyyyMMdd",
                                                    CultureInfo.InvariantCulture,
                                                    DateTimeStyles.None);
            }
            else
            {
                beforeDate = DateTime.ParseExact("17540101",
                                                    "yyyyMMdd",
                                                    CultureInfo.InvariantCulture,
                                                    DateTimeStyles.None);
                afterDate = DateTime.ParseExact("99990101",
                                                    "yyyyMMdd",
                                                    CultureInfo.InvariantCulture,
                                                    DateTimeStyles.None);
            }

            if (types.Count() > 0)
            {
                command.CommandText = command.CommandText.Replace("__WHERE_IN__", "OR type IN (" + String.Join(",", types.Select(t => (int)t)) + ")");
            }
            else
            {
                command.CommandText = command.CommandText.Replace("__WHERE_IN__", "");
            }

            command.Parameters.Add(new SqlParameter("p1", keyword + "%"));
            command.Parameters.Add(new SqlParameter("location", location + "%"));
            command.Parameters.Add(new SqlParameter("minYear", beforeDate));
            command.Parameters.Add(new SqlParameter("maxYear", afterDate));
            command.Parameters.Add(new SqlParameter("minAlcool", minAlcool));
            command.Parameters.Add(new SqlParameter("maxAlcool", maxAlcool));
            command.Parameters.Add(new SqlParameter("minVolume", minVolume));
            command.Parameters.Add(new SqlParameter("maxVolume", maxVolume));
            command.Parameters.Add(new SqlParameter("type", types.Count()));
            command.Parameters.Add(new SqlParameter("enstock", enstock));
            command.Parameters.Add(new SqlParameter("limit", limit));

            

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return new Bouteille()
                {
                    BouteilleId = (long)reader["bouteille_id"],
                    Label = (string)reader["label"],
                    Type = (WineType)((int)reader["type"]),
                    DegreeAlcool = (decimal)reader["degree"],
                    Volume = (decimal)reader["volume"],
                    Date = (DateTime)reader["date"],
                    Marque = (string)reader["marque"],
                    Origine = (string)reader["origine"],
                    Pays = (string)reader["pays"],
                    EnStock = (bool)reader["enstock"],
                    Review = (string)reader["review"],
                    FournisseurId = (long)reader["fournisseur_id"],
                    EmplacementId = (long)reader["emplacement_id"]
                };
            }
            reader.Close();
        }

        public void Add(Bouteille bouteille)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"INSERT INTO {TableName} VALUES (@p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13)";
            command.Parameters.Add(new SqlParameter("p2", bouteille.Label));
            command.Parameters.Add(new SqlParameter("p3", bouteille.Type));
            command.Parameters.Add(new SqlParameter("p4", bouteille.DegreeAlcool));
            command.Parameters.Add(new SqlParameter("p5", bouteille.Volume));
            command.Parameters.Add(new SqlParameter("p6", bouteille.Date));
            command.Parameters.Add(new SqlParameter("p7", bouteille.Marque));
            command.Parameters.Add(new SqlParameter("p8", bouteille.Origine));
            command.Parameters.Add(new SqlParameter("p9", bouteille.Pays));
            command.Parameters.Add(new SqlParameter("p10", bouteille.EnStock));
            command.Parameters.Add(new SqlParameter("p11", bouteille.Review));
            command.Parameters.Add(new SqlParameter("p12", bouteille.FournisseurId));
            command.Parameters.Add(new SqlParameter("p13", bouteille.EmplacementId));

            command.ExecuteNonQuery();
        }

        public void Edit(Bouteille bouteille)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"UPDATE {TableName} " +
                                  $"SET label = @p2, type = @p3, degree = @p4, volume = @p5, date = @p6," +
                                  $" marque = @p7, origine = @p8, pays = @p9, enstock = @p10, review = @p11," +
                                  $" fournisseur_id = @p12, emplacement_id = @p13" +
                                  $" WHERE {TableName}_id = @p1";
            command.Parameters.Add(new SqlParameter("p1", bouteille.BouteilleId));
            command.Parameters.Add(new SqlParameter("p2", bouteille.Label));
            command.Parameters.Add(new SqlParameter("p3", (int) bouteille.Type));
            command.Parameters.Add(new SqlParameter("p4", bouteille.DegreeAlcool));
            command.Parameters.Add(new SqlParameter("p5", bouteille.Volume));
            command.Parameters.Add(new SqlParameter("p6", bouteille.Date));
            command.Parameters.Add(new SqlParameter("p7", bouteille.Marque));
            command.Parameters.Add(new SqlParameter("p8", bouteille.Origine));
            command.Parameters.Add(new SqlParameter("p9", bouteille.Pays));
            command.Parameters.Add(new SqlParameter("p10", bouteille.EnStock));
            command.Parameters.Add(new SqlParameter("p11", bouteille.Review));
            command.Parameters.Add(new SqlParameter("p12", bouteille.FournisseurId));
            command.Parameters.Add(new SqlParameter("p13", bouteille.EmplacementId));
            command.ExecuteNonQuery();
        }

        public void EditStock(Bouteille bouteille)
        {
            DbCommand command = _connection.CreateCommand();
            command.CommandText = $"UPDATE {TableName} " +
                                  $"SET enstock = @p2" +
                                  $" WHERE {TableName}_id = @p1";
            command.Parameters.Add(new SqlParameter("p1", bouteille.BouteilleId));
            command.Parameters.Add(new SqlParameter("p2", false));
            command.ExecuteNonQuery();
        }

        protected override Bouteille ToEntity(DbDataReader reader)
        {
            return new Bouteille()
            {
                BouteilleId = (long)reader["bouteille_id"],
                Label = (string)reader["label"],
                Type = (WineType) ((int)reader["type"]),
                DegreeAlcool = (decimal)reader["degree"],
                Volume = (decimal)reader["volume"],
                Date = (DateTime)reader["date"],
                Marque = (string)reader["marque"],
                Origine = (string)reader["origine"],
                Pays = (string)reader["pays"],
                EnStock = (bool)reader["enstock"],
                Review = (string)reader["review"],
                FournisseurId = (long)reader["fournisseur_id"],
                EmplacementId = (long)reader["emplacement_id"]
            };
        }
    }
}
