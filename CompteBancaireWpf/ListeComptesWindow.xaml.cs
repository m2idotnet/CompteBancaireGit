using CompteBancaireWpf.Classes;
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
            listViewCompte.ItemsSource = Compte.GetComptes();
            bNewCompte.Click += (sender, e) =>
            {
                NewCompteWindow w = new NewCompteWindow(listViewCompte);
                w.Show();
            };
            bDepot.Click += (sender, e) =>
            {
                StartOperation(TypeOperation.Depot);
            };
            bRetrait.Click += (sender, e) =>
            {
                StartOperation(TypeOperation.Retrait);
            };
            bOperation.Click += (sender, e) =>
           {
               Compte c = listViewCompte.SelectedItem as Compte;
               if (c != null)
               {
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
