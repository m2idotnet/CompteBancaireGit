using CompteBancaireWpf.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompteBancaireWpf.ViewModels
{
    public class ListOperationsViewModel
    {
        public Compte compte { get; set; }
        public List<Operation> listeOperations { get; set; }

        public ListOperationsViewModel(Compte c)
        {
            compte = c;
            listeOperations = Operation.GetOperations(compte.Id);
        }
    }
}
