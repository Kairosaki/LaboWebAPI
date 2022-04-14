using LaboConsoAPI.Models;
using Newtonsoft.Json;

Console.WriteLine("Veuillez choisir parmi 1 à 4 : " +
                  "\n   1. Trouver toutes les bouteilles via fournisseur " +
                  "\n   2. Trouver toutes les bouteilles en stock" +
                  "\n   3. Trouver tous les emplacements libres " +
                  "\n   4. Quitter le programme avec la touche q");

int.TryParse(Console.ReadLine(), out int choix);



switch (choix)
{
    case 1: GetBouteillesByFournisseurName(); break;
    case 2: GetAllBouteillesInStock(); break;
    case 3: GetBouteillesByFournisseurName(); break;
    case 4: GetBouteillesByFournisseurName(); break;
    default: Console.WriteLine("Ce choix n'existe pas"); break;
}

void GetAllBouteillesInStock()
{
    Console.WriteLine("Veuillez entrer le nom du fournisseur");
    string? nomFournisseur = Console.ReadLine();
    if (nomFournisseur is null)
    {
        Console.WriteLine("Erreur veuillez entrer un nom de fournisseur");
        nomFournisseur = Console.ReadLine();
    }
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = client.GetAsync($"https://localhost:7006/api/Bouteille?Keyword={nomFournisseur}").Result;
    }
}

void GetBouteillesByFournisseurName()
{
    Console.WriteLine("Veuillez entrer le nom du fournisseur");
    string? nomFournisseur = Console.ReadLine();
    if (nomFournisseur is null)
    {
        Console.WriteLine("Erreur veuillez entrer un nom de fournisseur");
        nomFournisseur = Console.ReadLine();
    }
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = client.GetAsync($"https://localhost:7006/api/Bouteille?Keyword={nomFournisseur}").Result;

        if (response.IsSuccessStatusCode)
        {
            string json = response.Content.ReadAsStringAsync().Result;

            IEnumerable<Bouteille>? bouteilles = JsonConvert.DeserializeObject<IEnumerable<Bouteille>>(json);

            if (bouteilles is null || !bouteilles.Any())
            {
                Console.WriteLine("Aucun résultat");
            }
            else
            {
                foreach(Bouteille b in bouteilles)
                {
                    Console.WriteLine("*******************");
                    Console.WriteLine(b.ToString());
                    Console.WriteLine("*******************");
                }
            }
        }      

    }
}

