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
    /// Logique d'interaction pour NewCompteWindow.xaml
    /// </summary>
    public partial class NewCompteWindow : Window
    {

        private ListView listViewComptes { get; set; }
        public NewCompteWindow()
        {
            InitializeComponent();
        }

        public NewCompteWindow(ListView l)
        {
            InitializeComponent();
            listViewComptes = l;
            bAdd.Click += (sender, e) =>
            {
                Client c = new Client(nom.Text, prenom.Text, tel.Text);
                c.Add();
                if(c.Id > 0)
                {
                    Compte compte = new Compte()
                    {
                        ClientId = c.Id
                    };
                    compte.Add();
                    if(compte.Id > 0)
                    {
                        message.Content = "le compte a été ajouté voici son numéro " + compte.NumeroCompte;
                        listViewComptes.ItemsSource = Compte.GetComptes();
                    }
                    else
                    {
                        message.Content = "Erreur d'insertion de compte";
                    }
                }
                else
                {
                    message.Content = "Erreur d'insertion du client";
                }
            };
        }
    }
}
