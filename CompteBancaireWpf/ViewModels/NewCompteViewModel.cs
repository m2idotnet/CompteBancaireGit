using CompteBancaireWpf.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompteBancaireWpf.ViewModels
{
    public class NewCompteViewModel
    {
        public Client client { get; set; }
        public Compte compte { get; set; }
        public string message { get; set; }

        public NewCompteViewModel()
        {
            client = new Client();
            compte = new Compte();
        }
    }
}
