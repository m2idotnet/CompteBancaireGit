using System;
using System.Collections.Generic;
using System.Text;

namespace compteBancaire.Classes
{
    public static class Journalisation
    {
        private static List<Compte> comptes;
        //public static List<Compte> Comptes
        //{
        //    get
        //    {
        //        if (File.Exists(file))
        //        {
        //            StreamReader reader = new StreamReader(file);
        //            string contenu = reader.ReadToEnd();
        //            reader.Close();
        //            List<Compte> l = JsonConvert.DeserializeObject<List<Compte>>(contenu);
        //            comptes =  (l== null)? new List<Compte>() : l;

        //        }
        //        else
        //        {
        //            comptes =  new List<Compte>();
        //        }

        //        return comptes;
        //    }
        //    set
        //    {
        //        comptes = value;
        //    }
        //}
        public static List<Compte> Comptes;
        private static string file = "comptes.json";
        public static void AjouterCompteBancaire(Compte c)
        {
            Comptes.Add(c);
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
