using CompteBancaireSingleWindowMVVM.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompteBancaireSingleWindowMVVM.ViewModels
{
    public class ListeOperationsViewModel : ViewModelBase
    {
         
        public string NumeroCompte
        {
            get;
            set;
        }
        private Compte compte;

        public ObservableCollection<Operation> listeOperations { get; set; }
        public ICommand getOperationsCommand { get; set; } 
        public ListeOperationsViewModel()
        {
            getOperationsCommand = new RelayCommand(GetOperations);
        }

        public void GetOperations()
        {
            compte = new Compte(NumeroCompte);
            listeOperations = Operation.GetOperations(compte.Id);
            RaisePropertyChanged("listeOperations");
        }
    }
}
