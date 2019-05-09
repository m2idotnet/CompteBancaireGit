using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CompteBancaireAdoNetDeconnecte.Classes
{
    public class Operation
    {
        private int id;
        private int compteId;
        private DateTime dateOperation;
        private decimal montant;

        public int Id { get => id; set => id = value; }
        public int CompteId { get => compteId; set => compteId = value; }
        public DateTime DateOperation { get => dateOperation; set => dateOperation = value; }
        public decimal Montant { get => montant; set => montant = value; }

        public Operation()
        {

        }
        public Operation(decimal m, int c)
        {
            Montant = m;
            CompteId = c;
            DateOperation = DateTime.Now;
        }

        public void Add()
        {
            DataRow r = DataBase.Instance.Tables["Operation"].NewRow();
            r["Id"] = (DataBase.Instance.Tables["Operation"].Rows.Count > 0) ? (int)DataBase.Instance.Tables["Operation"].Rows[DataBase.Instance.Tables["Operation"].Rows.Count - 1]["id"] + 1 : 1;
            r["CompteId"] = CompteId;
            r["Montant"] = Montant;
            r["DateOperation"] = DateOperation;
            Id = (int)r["Id"];
            DataBase.Instance.Tables["Operation"].Rows.Add(r);
        }

        public static List<Operation> GetOperations(int compte)
        {
            List<Operation> liste = new List<Operation>();
            foreach(DataRow r in DataBase.Instance.Tables["Operation"].Rows)
            {
                Operation o = new Operation()
                {
                    Id = (int)r["Id"],
                    CompteId = (int)r["CompteId"],
                    Montant = (decimal)r["Montant"],
                    DateOperation = (DateTime)r["DateOperation"]
                };
                liste.Add(o);
            }
            
            return liste;
        }
        public override string ToString()
        {
            return "Date Opération : " + DateOperation + " Montant : " + Montant;
        }
    }
}
