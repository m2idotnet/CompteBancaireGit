using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CompteBancaireAdoNetDeconnecte.Classes
{
    public class Client
    {
        private int id;
        private string nom;
        private string prenom;
        private string tel;

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Tel { get => tel; set => tel = value; }

        public Client()
        {

        }
        public Client(string n, string p, string t)
        {
            Nom = n;
            Prenom = p;
            Tel = t;
        }

        public Client(int id)
        {
            foreach(DataRow r in DataBase.Instance.Tables["Client"].Rows)
            {
                if((int)r["id"] == id)
                {
                    Id = id;
                    Nom = (string)r["Nom"];
                    Prenom = (string)r["Prenom"];
                    Tel = (string)r["Tel"];
                    break;
                }
            } 
        }

        public void Add()
        {
            DataRow r = DataBase.Instance.Tables["Client"].NewRow();
            r["id"] = (DataBase.Instance.Tables["Client"].Rows.Count > 0 )? (int)DataBase.Instance.Tables["Client"].Rows[DataBase.Instance.Tables["Client"].Rows.Count - 1]["id"] + 1 : 1;
            r["Nom"] = Nom;
            r["Prenom"] = Prenom;
            r["Tel"] = Tel;
            DataBase.Instance.Tables["Client"].Rows.Add(r);
        }


        public override string ToString()
        {
            return "Nom : " + Nom + " Prénom : " + Prenom + " Tel : " + Tel;
        }
    }
}
