using CompteBancaireSingleWindowMVVM.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CompteBancaireSingleWindowMVVM.ViewModels
{
    public class MainWindowViewModel
    {
        private Grid maGrille { get; set; }
        public ICommand listeComptesCommand { get; set; }
        public ICommand addCompteCommand { get; set; }
        public ICommand operationCommand { get; set; }

        public ICommand listeOperationsCommand { get; set; }

        public MainWindowViewModel(Grid g)
        {
            maGrille = g;
            listeComptesCommand = new RelayCommand(ListeComptes);
            operationCommand = new RelayCommand<string>(Operation);
        }

        public void Operation(string type)
        {
            ResetGrid();
            Button b = new Button
            {
                Content = type
            };
            maGrille.Children.Add(b);
        }


        public void ListeComptes()
        {
            ResetGrid();
            ListView listView = new ListView();
            GridView gridView = new GridView();
            maGrille.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            maGrille.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            foreach(PropertyInfo pInfo in typeof(Compte).GetProperties())
            {
                gridView.Columns.Add(new GridViewColumn { Header = pInfo.Name, Width = pInfo.Name.Length * 20, DisplayMemberBinding = new Binding(pInfo.Name) });
            }
            listView.View = gridView;
            //premiere solution on passe la listView comme parametre au ViewModel et on met à jour le itemsSource à l'interieur du Task en invoquant le thread principal
            //maGrille.DataContext = new ListeComptesViewModel(listView);
            //Deuxieme solution
            maGrille.DataContext = new ListeComptesViewModel();
            listView.SetBinding(ListView.ItemsSourceProperty, new Binding("listeComptes"));
            maGrille.Children.Add(listView);
            Grid.SetRow(listView, 1);
            Label lMessage = new Label
            {

            };
            lMessage.SetBinding(Label.ContentProperty, new Binding("Message"));
            Grid.SetRow(lMessage, 0);
            maGrille.Children.Add(lMessage);
        }

        private void ResetGrid()
        {
            maGrille.Children.Clear();
            maGrille.RowDefinitions.Clear();
            maGrille.ColumnDefinitions.Clear();
        }
    }
}
