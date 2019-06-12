using CompteBancaireWpf.Classes;
using CompteBancaireWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CompteBancaireWpf
{
    /// <summary>
    /// Logique d'interaction pour ListeComptesWindow.xaml
    /// </summary>
    public partial class ListeComptesWindow : Window
    {
        public ListeComptesWindow()
        {
            InitializeComponent();
            //creation du datacontext pour les viewmodels
            ListeComptesViewModel v = new ListeComptesViewModel();
            DataContext = v;

            //implémetation du click nouveau compte
            bNewCompte.Click += (sender, e) =>
            {
                NewCompteWindow w = new NewCompteWindow(listViewCompte);
                w.Show();
            };

            //implémetation du click dépot
            bDepot.Click += (sender, e) =>
            {
                //appel de methode d'affichage de la fenêtre de dépot 
                StartOperation(TypeOperation.Depot);
            };

            //implémetation du click retrait
            bRetrait.Click += (sender, e) =>
            {
                //appel de methode d'affichage de la fenêtre de retrait
                StartOperation(TypeOperation.Retrait);
            };

            //implémetation du click opération
            bOperation.Click += (sender, e) =>
           {
               //création d'instance c à partir de la séléction de la liste transtypée en compte
               Compte c = listViewCompte.SelectedItem as Compte;
               if (c != null)
               {
                   //création d'une instance de la fenêtre listoperation + affichage de la fenêtre
                   ListOperationsWindow w = new ListOperationsWindow(c);
                   w.Show();
               }
               else
               {
                   MessageBox.Show("Merci de choisir un compte");
               }
           };
        }

        private void StartOperation(TypeOperation t)
        {
            Compte c = listViewCompte.SelectedItem as Compte;
            if (c != null)
            {
                //création d'une instance de la fenêtre operation + affichage de la fenêtre
                OperationWindow w = new OperationWindow(c, listViewCompte, t);
                w.Show();
            }
            else
            {
                MessageBox.Show("Merci de choisir un compte");
            }
        }
    }
}
