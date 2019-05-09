
using CompteBancaireAdoNetDeconnecte.Classes;
using System;
using System.Text;

namespace CompteBancaireAdoNetDeconnecte
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            DataBase.Init();
            Menu();
        }

        static void Menu()
        {
            Console.WriteLine("----Bienvenue sur le système de gestion de compte bancaire-----");
            int choix;
            do
            {
                Console.WriteLine("1- Ajouter un compte bancaire");
                Console.WriteLine("2- Déposer argent sur un compte");
                Console.WriteLine("3- Retirer argent d'un compte");
                Console.WriteLine("4-Afficher opération d'un compte bancaire");
                Console.WriteLine("0-Quitter");
                choix = Convert.ToInt32(Console.ReadLine());
                switch (choix)
                {
                    case 1:
                        CreerCompte();
                        break;
                    case 2:
                        Deposer();
                        break;
                    case 3:
                        Retirer();
                        break;
                    case 4:
                        AfficherOperation();
                        break;
                    case 0:
                        DataBase.Update();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Pas de fonction");
                        break;
                }
            } while (choix != 0);
        }

        static void CreerCompte()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--Création Compte bancaire------");
            Console.ResetColor();
            Console.Write("Nom du client : ");
            string nom = Console.ReadLine();
            Console.Write("Prénom du client : ");
            string prenom = Console.ReadLine();
            Console.Write("Tél du client : ");
            string tel = Console.ReadLine();
            Client c = new Client(nom, prenom, tel);
            c.Add();
            Compte compte = new Compte() { ClientId = c.Id };
            compte.Add();
            Console.WriteLine("Compte crée");
        }

        static void Deposer()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------Dépot---------");
            Console.ResetColor();
            Console.Write("Numéro de compte : ");
            string numero = Console.ReadLine();
            Compte compte = new Compte(numero);
            if (compte.Id > 0)
            {
                Client c = new Client(compte.ClientId);
                Console.Write("Montant du dépot : ");
                decimal depot = Convert.ToDecimal(Console.ReadLine());
                Operation o = new Operation(depot, compte.Id);
                o.Add();
                if (o.Id > 0)
                {
                    compte.Solde += depot;
                    compte.Update();
                    Console.WriteLine("Opération effectuée");
                }
            }
            else
            {
                Console.WriteLine("Aucun compte avec ce numero");
            }
        }

        static void Retirer()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------Retrait---------");
            Console.ResetColor();
            Console.Write("Numéro de compte : ");
            string numero = Console.ReadLine();
            Compte compte = new Compte(numero);
            if (compte.Id > 0)
            {
                Client c = new Client(compte.ClientId);
                Console.Write("Montant du retrait : ");
                decimal retrait = Convert.ToDecimal(Console.ReadLine());
                if (retrait <= compte.Solde)
                {
                    Operation o = new Operation(retrait * -1, compte.Id);
                    o.Add();
                    if (o.Id > 0)
                    {
                        compte.Solde += retrait * -1;
                        compte.Update();
                        Console.WriteLine("Opération effectuée");
                    }
                }
                else
                {
                    Console.WriteLine("Solde insuffisant");
                }
            }
            else
            {
                Console.WriteLine("Aucun compte avec ce numero");
            }
        }

        static void AfficherOperation()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------Liste opérations---------");
            Console.ResetColor();
            Console.Write("Numéro de compte : ");
            string numero = Console.ReadLine();
            Compte compte = new Compte(numero);
            if (compte.Id > 0)
            {

                Client c = new Client(compte.ClientId);
                Console.WriteLine(c);
                foreach (Operation o in Operation.GetOperations(compte.Id))
                {
                    Console.WriteLine(o);
                    Console.WriteLine("--------------------------------");
                }
                Console.WriteLine("Solde : " + compte.Solde);
            }
            else
            {
                Console.WriteLine("Aucun compte avec ce numero");
            }
        }
    }
}
