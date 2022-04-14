using LaboConsoAPI.Models;
using Newtonsoft.Json;



Console.WriteLine("Veuillez choisir parmi 1 à 4 : " +
                  "\n   1. Trouver toutes les bouteilles via fournisseur " +
                  "\n   2. Trouver toutes les bouteilles en stock" +
                  "\n   3. Trouver tous les emplacements libres " +
                  "\n   4. Quitter le programme avec la touche q");
int.TryParse(Console.ReadLine(), out int choix);

while (choix != 4)
{
    switch (choix)
    {
        case 1: GetBouteillesByFilter(); choix = AskAgain(); break;
        case 2: GetAllBouteillesByEtagereName(); choix = AskAgain(); break;
        case 3: GetEmplacementsLibres(); choix = AskAgain(); break;
        case 4: Environment.Exit(0); break;
        default: Console.WriteLine("Ce choix n'existe pas"); break;
    }
    
}

void GetEmplacementsLibres()
{
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = client.GetAsync($"https://localhost:7006/api/Emplacement/libres").Result;

        if (response.IsSuccessStatusCode)
        {
            string json = response.Content.ReadAsStringAsync().Result;

            IEnumerable<Emplacement>? emplacements = JsonConvert.DeserializeObject<IEnumerable<Emplacement>>(json);

            if (emplacements is null || !emplacements.Any())
            {
                Console.WriteLine("Aucun résultat");
            }
            else
            {
                foreach (Emplacement e in emplacements)
                {
                    Console.WriteLine("*******************");
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("*******************");
                }
            }
        }
    }
}

void GetBouteillesByFilter()
{
    Console.WriteLine("Veuillez entrer un filtre : label, nom fournisseur, pays etc");
    string? filter = Console.ReadLine();
    if (filter is null)
    {
        Console.WriteLine("Erreur veuillez entrer un nom de fournisseur");
        filter = Console.ReadLine();
    }
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = client.GetAsync($"https://localhost:7006/api/Bouteille?Keyword={filter}").Result;

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
                foreach (Bouteille b in bouteilles)
                {
                    Console.WriteLine("*******************");
                    Console.WriteLine(b.ToString());
                    Console.WriteLine("*******************");
                }
            }
        }

    }
}

void GetAllBouteillesByEtagereName()
{
    Console.WriteLine("Veuillez entrer le nom de l'étagère ex : E1, E2 etc.");
    string? etagereName = Console.ReadLine();
    while (etagereName is null)
    {
        Console.WriteLine("Erreur veuillez entrer un nom de l'étagère non vide");
        etagereName = Console.ReadLine();
    }
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = client.GetAsync($"https://localhost:7006/api/Bouteille?Location={etagereName}").Result;

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
                foreach (Bouteille b in bouteilles)
                {
                    Console.WriteLine("*******************");
                    Console.WriteLine(b.ToString());
                    Console.WriteLine("*******************");
                }
            }
        }
    }
}

int AskAgain()
{
    Console.WriteLine("Veuillez choisir parmi 1 à 4 : " +
                  "\n   1. Trouver toutes les bouteilles via fournisseur " +
                  "\n   2. Trouver toutes les bouteilles en stock" +
                  "\n   3. Trouver tous les emplacements libres " +
                  "\n   4. Quitter le programme avec la touche q");
    int choix;
    int.TryParse(Console.ReadLine(), out choix);    
    return choix;
}