using System;
using System.Collections.Generic;
using System.Text;

namespace compteBancaire.Classes
{
    public class Compte
    {
        private string numero;
        private decimal solde;
        private Client _client;
        private List<Operation> operations;


        public string Numero { get => numero; set => numero = value; }
        public decimal Solde { get => solde; set => solde = value; }
        public Client Client { get => _client; set => _client = value; }
        public List<Operation> Operations { get => operations; set => operations = value; }

        public Compte()
        {
            Numero = Guid.NewGuid().ToString();
            Operations = new List<Operation>();
            Client = new Client();
            Solde = 0;
        }

        public bool Deposer(decimal montant)
        {
            bool retour = true;
            Solde += montant;
            Operation o = new Operation() { Montant = montant };
            Operations.Add(o);
            return retour;
        }
        public bool Retirer(decimal montant)
        {
            bool retour = false;
            if(solde >= montant)
            {
                Solde -= montant;
                Operation o = new Operation() { Montant = montant * -1 };
                Operations.Add(o);
                retour = true;
            }
            return retour;
        }
    }
}
