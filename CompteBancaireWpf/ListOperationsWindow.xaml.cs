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
    /// Logique d'interaction pour ListOperationsWindow.xaml
    /// </summary>
    public partial class ListOperationsWindow : Window
    {
        private Compte compte { get; set; }
        public ListOperationsWindow(Compte  c)
        {
            InitializeComponent();
            compte = c;
            listViewOperation.ItemsSource = Operation.GetOperations(compte.Id);
            solde.Content = compte.Solde;
        }
    }
}
