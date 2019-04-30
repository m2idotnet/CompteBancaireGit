using compteBancaire.Classes;
using System;
using System.Text;

namespace compteBancaire
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
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
            Client c = new Client() { Nom = nom, Prenom = prenom, Tel = tel };
            Compte compte = new Compte() { Client = c };
            Journalisation.AjouterCompteBancaire(compte);
            Console.WriteLine("Compte crée");
            Console.WriteLine("Numéro de compte : " + compte.Numero);
        }

        static void Deposer()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------Dépot---------");
            Console.ResetColor();
            Console.Write("Numéro de compte : ");
            string numero = Console.ReadLine();
            Compte compte = Journalisation.GetCompteBancaire(numero);
            if (compte == null)
            {
                Console.WriteLine("Aucun compte avec ce numéro");
            }
            else
            {
                Console.Write("Montant du dépot : ");
                decimal depot = Convert.ToDecimal(Console.ReadLine());
                if (compte.Deposer(depot))
                {
                    Console.WriteLine("Dépot effecuté ");
                    Console.WriteLine("Nouveau solde : " + compte.Solde + " €");
                }
                else
                {
                    Console.WriteLine("Erreur serveur");
                }
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
            Compte compte = Journalisation.GetCompteBancaire(numero);
            if (compte == null)
            {
                Console.WriteLine("Aucun compte avec ce numéro");
            }
            else
            {
                Console.Write("Montant du retrait : ");
                decimal depot = Convert.ToDecimal(Console.ReadLine());
                if (compte.Retirer(depot))
                {
                    Console.WriteLine("Retrait effecuté ");
                    Console.WriteLine("Nouveau solde : " + compte.Solde + " €");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Pas de solde");
                    Console.ResetColor();
                }
            }
        }

        static void AfficherOperation()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------Affichage opérations---------");
            Console.ResetColor();
            Console.Write("Numéro de compte : ");
            string numero = Console.ReadLine();
            Compte compte = Journalisation.GetCompteBancaire(numero);
            if (compte == null)
            {
                Console.WriteLine("Aucun compte avec ce numéro");
            }
            else
            {
                Console.WriteLine("--Info Client--");
                Console.WriteLine("Nom et prénom : " + compte.Client.Nom + " " + compte.Client.Prenom);
                Console.WriteLine("Téléphone : " + compte.Client.Tel);
                Console.WriteLine("---Listes opérations");
                foreach(Operation o in compte.Operations)
                {
                    Console.WriteLine("Montant : " + o.Montant + " € Date : " + o.Date);
                }
            }
        }
    }
}
