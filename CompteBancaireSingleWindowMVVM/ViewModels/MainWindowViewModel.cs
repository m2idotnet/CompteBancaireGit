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
            listeOperationsCommand = new RelayCommand(ListeOperations);
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

        public void ListeOperations()
        {
            ResetGrid();
            for(int i = 1; i<=2; i++)
            {
                maGrille.RowDefinitions.Add(new RowDefinition
                {
                    Height = (i==1)  ? new GridLength(1, GridUnitType.Auto) : new GridLength(1, GridUnitType.Star)
                });
                maGrille.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = (i == 1) ? new GridLength(4, GridUnitType.Star) : new GridLength(1, GridUnitType.Star)
                });
            }
            maGrille.DataContext = new ListeOperationsViewModel();
            Label lNumero = new Label
            {
                Content = "Numero : ",
                HorizontalAlignment = HorizontalAlignment.Left
            };
            maGrille.Children.Add(lNumero);
            Grid.SetColumn(lNumero, 0);
            Grid.SetRow(lNumero, 0);
            TextBox tNumero = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Width=300
            };
            tNumero.SetBinding(TextBox.TextProperty, new Binding("NumeroCompte"));
            maGrille.Children.Add(tNumero);
            Grid.SetColumn(tNumero, 0);
            Grid.SetRow(tNumero, 0);
            Button bGetOperation = new Button
            {
                Content = "chercher operations",
            };
            bGetOperation.SetBinding(Button.CommandProperty, new Binding("getOperationsCommand"));
            maGrille.Children.Add(bGetOperation);
            Grid.SetColumn(bGetOperation, 1);
            Grid.SetRow(bGetOperation, 0);
            ListView listView = new ListView();
            GridView gridView = new GridView();
            foreach (PropertyInfo pInfo in typeof(Operation).GetProperties())
            {
                gridView.Columns.Add(new GridViewColumn { Header = pInfo.Name, Width = pInfo.Name.Length * 20, DisplayMemberBinding = new Binding(pInfo.Name) });
            }
            listView.View = gridView;
            listView.SetBinding(ListView.ItemsSourceProperty, new Binding("listeOperations"));
            maGrille.Children.Add(listView);
            Grid.SetColumn(listView, 0);
            Grid.SetRow(listView, 1);
            Grid.SetColumnSpan(listView, 2);
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
