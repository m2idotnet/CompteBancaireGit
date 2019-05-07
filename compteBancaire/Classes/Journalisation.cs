using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            Comptes = getListFromFile();
            Comptes.Add(c);
            UpdateCompte();
        }



        public static Compte GetCompteBancaire(string numero)
        {
            Comptes = getListFromFile();
            Compte compte = null;
            foreach (Compte c in Comptes)
            {
                if (c.Numero == numero)
                {
                    compte = c;
                    break;
                }
            }
            return compte;
        }

        public static void UpdateCompte()
        {
            StreamWriter writer = new StreamWriter(file);
            writer.Write(JsonConvert.SerializeObject(Comptes));
            writer.Close();
        }

        public static List<Compte> getListFromFile()
        {
            List<Compte> liste = new List<Compte>();
            if (File.Exists(file))
            {
                StreamReader reader = new StreamReader(file);
                liste = JsonConvert.DeserializeObject<List<Compte>>(reader.ReadToEnd());
                reader.Close();
            }
            return liste;
        }
    }
}
