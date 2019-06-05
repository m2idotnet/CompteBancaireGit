using CompteBancaireSingleWindowMVVM.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CompteBancaireSingleWindowMVVM.ViewModels
{
    public class ListeComptesViewModel : ViewModelBase
    {
        public ObservableCollection<Compte> listeComptes { get; set; }
        private string message;
        public string Message { get {
                return message;
            }
            set
            {
                message = value;
                RaisePropertyChanged();
            }
        }
        public ListeComptesViewModel(ListView l)
        {
            listeComptes = new ObservableCollection<Compte>();
           
            Task t = Task.Run(() =>
            {
                Thread.Sleep(3000);
                listeComptes = Compte.GetComptes();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    l.ItemsSource = listeComptes;
                });
            });
        }
        public ListeComptesViewModel()
        {
            Message = "En cours";
            listeComptes = new ObservableCollection<Compte>();
            Task t = Task.Run(() =>
            {
                Thread.Sleep(3000);
                listeComptes = Compte.GetComptes();
                RaisePropertyChanged("listeComptes");
                Message = "";
            });
        }
    }
}
