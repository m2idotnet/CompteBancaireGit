using System;
using System.Collections.Generic;
using System.Text;

namespace compteBancaire.Classes
{
    public static class Journalisation
    {
        private static List<Compte> Comptes = new List<Compte>();
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
