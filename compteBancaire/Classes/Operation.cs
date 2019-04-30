using System;
using System.Collections.Generic;
using System.Text;

namespace compteBancaire.Classes
{
    public class Operation
    {
        private string numero;
        private decimal montant;
        private DateTime date;

        public string Numero { get => numero; set => numero = value; }
        public decimal Montant { get => montant; set => montant = value; }
        public DateTime Date { get => date; set => date = value; }

        public Operation()
        {
            Numero = Guid.NewGuid().ToString();
            Date = DateTime.Now;
        }
    }
}
