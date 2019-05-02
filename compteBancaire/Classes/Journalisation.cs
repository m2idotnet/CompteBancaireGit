using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace compteBancaire.Classes
{
    public static class Journalisation
    {
        private static List<Compte> Comptes = new List<Compte>();
        public static void AjouterCompteBancaire(Compte c)
        {
            Comptes.Add(c);
            //serialize
            StreamWriter writer = new StreamWriter(@"C:\Users\Administrateur\Desktop\compte" + c.Numero + ".json");
            string compteenJson = JsonConvert.SerializeObject(c);
            writer.WriteLine(compteenJson);
            writer.Close();
        }

        public static Compte GetCompteBancaire(string numero)
        {
            Compte compte = null;
            foreach(Compte c in Comptes)
            {
                if(c.Numero == numero)
                {
                    compte = c;
                    break;
                }
            }
            return compte;
        }
    }
}
