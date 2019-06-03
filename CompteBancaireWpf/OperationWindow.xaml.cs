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
    /// Logique d'interaction pour OperationWindow.xaml
    /// </summary>
    public partial class OperationWindow : Window
    {
        private Compte compte { get; set; }
        private ListView listViewComptes { get; set; }
        private TypeOperation type { get; set; }
        public OperationWindow(Compte c, ListView l, TypeOperation t)
        {
            InitializeComponent();
            compte = c;
            listViewComptes = l;
            type = t;
            Title = type.ToString() + " N° : "+compte.NumeroCompte;
            bOperation.Click += (sender, e) =>
             {
                 if(type == TypeOperation.Depot)
                 {
                     Operation o = new Operation(Convert.ToDecimal(montant.Text), compte.Id);
                     o.Add();
                     if(o.Id > 0)
                     {
                         compte.Solde += o.Montant;
                         compte.Update();
                         message.Content = "Opération effectuée";
                         listViewComptes.ItemsSource = Compte.GetComptes();
                     }
                     else
                     {
                         message.Content = "Erreur operation";
                     }
                 }
                 else if(type == TypeOperation.Retrait)
                 {
                     Operation o = new Operation(Convert.ToDecimal(montant.Text) * -1, compte.Id);
                     if (compte.Solde >= o.Montant*-1)
                     {
                         o.Add();
                         if (o.Id > 0)
                         {
                             compte.Solde += o.Montant;
                             compte.Update();
                             message.Content = "Opération effectuée";
                             listViewComptes.ItemsSource = Compte.GetComptes();
                         }
                         else
                         {
                             message.Content = "Erreur operation";
                         }
                     }
                     else
                     {
                         message.Content = "pas de solde";
                     }
                 }
                 
             };
        }
    }

    public enum TypeOperation
    {
        Depot, 
        Retrait,
    }
}
