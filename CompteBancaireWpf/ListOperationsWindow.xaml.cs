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
    /// Logique d'interaction pour ListOperationsWindow.xaml
    /// </summary>
    public partial class ListOperationsWindow : Window
    {
        public ListOperationsWindow(Compte  c)
        {
            InitializeComponent();
            DataContext = new ListOperationsViewModel(c);
        }
    }
}
