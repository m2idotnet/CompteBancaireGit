using CompteBancaireWpf.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompteBancaireWpf.ViewModels
{
    public class ListeComptesViewModel
    {
        //observablecollection : permet de mettre a jour l'affichage des données
        public ObservableCollection<Compte> listeComptes { get; set; }

        public ListeComptesViewModel()
        {
            listeComptes = Compte.GetComptes();
        }
    }
}
