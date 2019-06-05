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
    /// Logique d'interaction pour NewCompteWindow.xaml
    /// </summary>
    public partial class NewCompteWindow : Window
    {

        private ListView listViewComptes { get; set; }
        public NewCompteWindow()
        {
            InitializeComponent();
        }

        //constructeur qui prend une liste comme argument
        public NewCompteWindow(ListView l)
        {
            InitializeComponent();
            //création instance datacontext pour appel de newcompteviewmodel
            NewCompteViewModel v = new NewCompteViewModel();
            DataContext = v;
            listViewComptes = l;
            bAdd.Click += (sender, e) =>
            {
                v.client.Add();
                if(v.client.Id > 0)
                {
                    v.compte.ClientId = v.client.Id;
                    v.compte.Add();
                    if(v.compte.Id > 0)
                    {
                        v.message = "le compte a été ajouté voici son numéro " + v.compte.NumeroCompte;
                        listViewComptes.ItemsSource = Compte.GetComptes();
                    }
                    else
                    {
                        v.message = "Erreur d'insertion de compte";
                    }
                }
                else
                {
                    v.message = "Erreur d'insertion du client";
                }
            };
        }
    }
}
