using System;
using System.Collections.Generic;
using System.Text;

namespace compteBancaire.Classes
{
    public class Client
    {
        private string nom;
        private string prenom;
        private string tel;

        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Tel { get => tel; set => tel = value; }
    }
}
