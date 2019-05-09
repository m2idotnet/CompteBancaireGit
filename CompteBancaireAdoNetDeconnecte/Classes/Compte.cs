using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CompteBancaireAdoNetDeconnecte.Classes
{
    public class Compte
    {
        private int id;
        private int clientId;
        private string numeroCompte;
        private decimal solde;

        public int Id { get => id; set => id = value; }
        public int ClientId { get => clientId; set => clientId = value; }
        public string NumeroCompte { get => numeroCompte; set => numeroCompte = value; }
        public decimal Solde { get => solde; set => solde = value; }

        public Compte()
        {
            NumeroCompte = Guid.NewGuid().ToString();
            Solde = 0;
        }

        public Compte(string numero)
        {
            foreach (DataRow r in DataBase.Instance.Tables["Compte"].Rows)
            {
                if ((string)r["Numero"] == numero)
                {
                    Id = (int)r["Id"];
                    NumeroCompte = numero;
                    Solde = (decimal)r["Solde"];
                    ClientId = (int)r["ClientId"];
                    break;
                }
            }
        }

        public void Add()
        {
            DataRow r = DataBase.Instance.Tables["Compte"].NewRow();
            r["id"] = (DataBase.Instance.Tables["Compte"].Rows.Count > 0) ? (int)DataBase.Instance.Tables["Compte"].Rows[DataBase.Instance.Tables["Compte"].Rows.Count - 1]["id"] + 1 : 1;
            r["Solde"] = Solde;
            r["ClientId"] = ClientId;
            r["Numero"] = NumeroCompte;

            DataBase.Instance.Tables["Compte"].Rows.Add(r);
        }

        public void Update()
        {
            foreach (DataRow r in DataBase.Instance.Tables["Compte"].Rows)
            {
                if ((int)r["Id"] == Id)
                {
                    r["Solde"] = Solde;
                    break;
                }
            }
        }
    }
}
